using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif
using StarterAssets;

[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
[RequireComponent(typeof(PlayerInput))]
#endif
public class QuakeFirstPersonController : MonoBehaviour
{
	[Header("Player")]
	[Tooltip("Move speed of the character in m/s")]
	public float MoveSpeed = 10.0f;
	[Tooltip("Rotation speed of the character")]
	public float RotationSpeed = 1.0f;
	[Tooltip("Ground Acceleration and deceleration")]
	public float GroundAccelerate = 10.0f;
	[Tooltip("Air Acceleration and deceleration")]
	public float AirAccelerate = 1.0f;
	[Tooltip("Ground Max Velocity")]
	public float GroundMaxVelocity = 100.0f;
	[Tooltip("Air Max Velocity")]
	public float AirMaxVelocity = 100.0f;
	[Tooltip("Friction for movement")]
	public float Friction = 5.0f;

	[Space(10)]
	[Tooltip("The height the player can jump")]
	public float JumpHeight = 1.2f;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	public float Gravity = -15.0f;

	[Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float JumpTimeout = 0.1f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float FallTimeout = 0.15f;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	public bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	public float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	public float GroundedRadius = 0.5f;
	[Tooltip("What layers the character uses as ground")]
	public LayerMask GroundLayers;

	[Header("Cinemachine")]
	[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
	public GameObject CinemachineCameraTarget;
	[Tooltip("How far in degrees can you move the camera up")]
	public float TopClamp = 90.0f;
	[Tooltip("How far in degrees can you move the camera down")]
	public float BottomClamp = -90.0f;

	// cinemachine
	private float _cinemachineTargetPitch;

	// player
	private float _speed;
	private float _rotationVelocity;
	private float _verticalVelocity;
	private float _terminalVelocity = 53.0f;

	// timeout deltatime
	private float _jumpTimeoutDelta;
	private float _fallTimeoutDelta;


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	private PlayerInput _playerInput;
#endif
	private CharacterController _controller;
	private StarterAssetsInputs _input;

	private const float _threshold = 0.01f;

	private bool IsCurrentDeviceMouse
	{
		get
		{
			#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
			return _playerInput.currentControlScheme == "KeyboardMouse";
			#else
			return false;
			#endif
		}
	}


	private void Start()
	{
		_controller = GetComponent<CharacterController>();
		_input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		_playerInput = GetComponent<PlayerInput>();
#else
		Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

		// reset our timeouts on start
		_jumpTimeoutDelta = JumpTimeout;
		_fallTimeoutDelta = FallTimeout;
	}

	private void Update()
	{
		GroundedCheck();
		Move();
	}

	private void LateUpdate()
	{
		CameraRotation();
	}

	private void GroundedCheck()
	{
		// set sphere position, with offset
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
		Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
	}

	private void CameraRotation()
	{
		// if there is an input
		if (_input.look.sqrMagnitude >= _threshold)
		{
			//Don't multiply mouse input by Time.deltaTime
			float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
			
			_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
			_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

			// clamp our pitch rotation
			_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

			// Update Cinemachine camera target pitch
			CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

			// rotate the player left and right
			transform.Rotate(Vector3.up * _rotationVelocity);
		}
	}

	private MovementParameters GetMovementParameters() {
		return new MovementParameters{
			air_accelerate = AirAccelerate,
			max_velocity_air = AirMaxVelocity, 
			ground_accelerate = GroundAccelerate, 
			max_velocity_ground = GroundMaxVelocity, 
			friction = Friction,
		};
	}

	private static float CalculateNewSpeed(bool isGrounded, Vector3 desiredVelocity, Vector3 prevVelocity, MovementParameters parameters) {
		prevVelocity = new Vector3(prevVelocity.x, 0.0f, prevVelocity.z);
		desiredVelocity = new Vector3(desiredVelocity.x, 0.0f, desiredVelocity.z);
		Vector3 newVelocity;
		if (isGrounded) {
			newVelocity = MovementUtils.MoveGround(desiredVelocity, prevVelocity, parameters);
		} else {
			newVelocity = MovementUtils.MoveAir(desiredVelocity, prevVelocity, parameters);
		}
		return newVelocity.magnitude;
	}

	private static Vector3 CalculateNormalizedMotion(Vector2 inputMove, Transform playerOrientation) {
		if (inputMove == Vector2.zero)
		{
			return Vector3.zero;
		}
		var motion = playerOrientation.right * inputMove.x + playerOrientation.forward * inputMove.y;
		return motion.normalized;
	}

	private static Vector3 FindTargetVelocity(Vector2 inputMove, Transform playerOrientation, float defaultMoveSpeed) {
		if (inputMove == Vector2.zero) {
			return Vector3.zero;
		}
		var normalizedMotion = CalculateNormalizedMotion(inputMove, playerOrientation);
		var scaledMotion = normalizedMotion * defaultMoveSpeed;
		return scaledMotion;
	}

	private void Move()
	{
		JumpAndGravity();
		Vector3 targetVelocity = FindTargetVelocity(_input.move, transform, MoveSpeed);
		Vector3 prevVelocity = _controller.velocity;
		Vector2 inputMove = _input.move;
		Vector3 normalizedMotion = CalculateNormalizedMotion(inputMove, transform);

		Debug.Log("Target vel " + targetVelocity + " prev vel " + prevVelocity + " input move " + inputMove);

		_speed = CalculateNewSpeed(Grounded, targetVelocity, prevVelocity, GetMovementParameters());
		_controller.Move(normalizedMotion * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
	}

	private void JumpAndGravity()
	{
		if (Grounded)
		{
			// reset the fall timeout timer
			_fallTimeoutDelta = FallTimeout;

			// stop our velocity dropping infinitely when grounded
			if (_verticalVelocity < 0.0f)
			{
				_verticalVelocity = -2f;
			}

			// Jump
			if (_input.jump && _jumpTimeoutDelta <= 0.0f)
			{
				// the square root of H * -2 * G = how much velocity needed to reach desired height
				_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
			}

			// jump timeout
			if (_jumpTimeoutDelta >= 0.0f)
			{
				_jumpTimeoutDelta -= Time.deltaTime;
			}
		}
		else
		{
			// reset the jump timeout timer
			_jumpTimeoutDelta = JumpTimeout;

			// fall timeout
			if (_fallTimeoutDelta >= 0.0f)
			{
				_fallTimeoutDelta -= Time.deltaTime;
			}

			// if we are not grounded, do not jump
			_input.jump = false;
		}

		// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		if (_verticalVelocity < _terminalVelocity)
		{
			_verticalVelocity += Gravity * Time.deltaTime;
		}
	}

	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	public float GetSpeed()
	{
		var velocity = _controller.velocity;
		var horizontalVelocity = new Vector3(velocity.x, 0.0f, velocity.z);
		return horizontalVelocity.magnitude;
	}
}