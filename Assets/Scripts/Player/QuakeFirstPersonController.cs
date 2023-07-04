using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class QuakeFirstPersonController : IPlayerController
{
	[Header("Player")]
	[SerializeField] private MovementParameters movementParameters;
	[Tooltip("Rotation speed of the character")]
	public float RotationSpeed = 1.0f;

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
	private float _speed = 0f;
	private float _rotationVelocity = 0f;
	private float _verticalVelocity = 0f;

	private PlayerInput _playerInput;
	private CharacterController _controller;
	private StarterAssetsInputs _input;

	private const float _threshold = 0.01f;

	private bool IsCurrentDeviceMouse
	{
		get
		{
			return _playerInput.currentControlScheme == "KeyboardMouse";
		}
	}


	private void Start()
	{
		_controller = GetComponent<CharacterController>();
		_input = GetComponent<StarterAssetsInputs>();
		_playerInput = GetComponent<PlayerInput>();
	}

	private void Update()
	{
		GroundedCheck();
		var (isGrounded, verticalSpeed) = Jump();
		Move(isGrounded, verticalSpeed);
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

	private (bool, float) Jump() {
		var isGrounded = Grounded;
		var terminalVelocity = movementParameters.terminalVelocity;
		var gravity = movementParameters.gravity;
		var currentVerticalVelocity = _verticalVelocity;
		var timeFrame = Time.deltaTime;
		if (!isGrounded) {
			var newVerticalVelocity = Mathf.Max(terminalVelocity, currentVerticalVelocity + gravity * timeFrame);
			return (false, newVerticalVelocity);
		}
		if (_input.jump) {
			return (false, movementParameters.jumpHeight);
		}
		return (true, 0f);
	}

	private static Vector3 CalculateWishDir(Vector2 inputMove, Transform playerOrientation) {
		if (inputMove == Vector2.zero)
		{
			return Vector3.zero;
		}
		var motion = playerOrientation.right * inputMove.x + playerOrientation.forward * inputMove.y;
		Debug.Assert(Mathf.Abs(motion.y) < 0.1, "motion is flat");
		return motion.normalized;
	}

	private static Vector3 friction(Vector3 vel, float frictionCoeff, float timeFrame) {
		if (vel.magnitude < 0.1f)
		{
			return Vector3.zero;
		}
		var speed = vel.magnitude;
		var drop = speed * frictionCoeff * timeFrame;
		var scaled = vel * Mathf.Max(speed - drop, 0) / speed;
		return scaled;
	}

	private static Vector3 accelerate(Vector3 wishDir, Vector3 vel, float accel, float maxSpeed, float timeFrame) {
		var currentSpeed = Vector3.Dot(vel, wishDir);
		var addSpeed = Mathf.Clamp(maxSpeed - currentSpeed, 0, accel * timeFrame);
		var accelerated = vel + wishDir * addSpeed;
		return accelerated;
	}

	private void Move(bool isGrounded, float verticalVelocity)
	{
		var inputMove = _input.move;
		var wishDir = CalculateWishDir(inputMove, transform);
		var vel = _controller.velocity;
		var timeFrame = Time.deltaTime;
		var acceleration = isGrounded ? movementParameters.groundAcceleration : movementParameters.airAcceleration;
		var maxSpeed = isGrounded ? movementParameters.maxSpeedGround : movementParameters.maxSpeedAir;

		var newVel = new Vector3(vel.x, 0, vel.z);
		if (isGrounded) {
			newVel = friction(newVel, movementParameters.friction, timeFrame);
		}
		newVel = accelerate(wishDir, newVel, acceleration, maxSpeed, timeFrame);
		Debug.Assert(Mathf.Abs(wishDir.y) < 0.1, "wish dir is flat");
		Debug.Assert(Mathf.Abs(newVel.y) < 0.1, "new vel is flat");
		var verticalVel = new Vector3(0, verticalVelocity, 0);
		_speed = newVel.magnitude;
		_verticalVelocity = verticalVelocity;
		_controller.Move(newVel * timeFrame + verticalVel * timeFrame);
	}

	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	public override float GetSpeed()
	{
		var velocity = _controller.velocity;
		var horizontalVelocity = new Vector3(velocity.x, 0.0f, velocity.z);
		return horizontalVelocity.magnitude;
	}
}