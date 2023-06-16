[System.Serializable]
public class MovementParameters {
	public float airAcceleration = 60.0f;
	public float maxSpeedAir = 0.6f;
	public float groundAcceleration = 60.0f;
	public float maxSpeedGround = 6.0f;
	public float friction = 3.5f;

	public float jumpHeight = 1.2f;
	public float gravity = -15f;
	public float terminalVelocity = -75f;
}