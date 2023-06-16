[System.Serializable]
public class MovementParameters {
	public float air_accelerate = 60.0f;
	public float max_velocity_air = 0.6f;
	public float ground_accelerate = 60.0f;
	public float max_velocity_ground = 6.0f;
	public float friction = 3.5f;

	public float jumpHeight = 1.2f;
	public float gravity = -15f;
}