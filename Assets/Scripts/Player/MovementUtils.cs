using UnityEngine;
// Source: https://adrianb.io/2015/02/14/bunnyhop.html
class MovementUtils {
    // accelDir: normalized direction that the player has requested to move (taking into account the movement keys and look direction)
    // prevVelocity: The current velocity of the player, before any additional calculations
    // accelerate: The server-defined player acceleration value
    // max_velocity: The server-defined maximum player velocity (this is not strictly adhered to due to strafejumping)
    static public Vector3 MovementAfterAcceleration(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float max_velocity)
    {
        float projVel = Vector3.Dot(prevVelocity, accelDir); // Vector projection of Current velocity onto accelDir.
        float accelVel = accelerate * Time.fixedDeltaTime; // Accelerated velocity in direction of movment

        // If necessary, truncate the accelerated velocity so the vector projection does not exceed max_velocity
        if(projVel + accelVel > max_velocity)
            accelVel = max_velocity - projVel;

        return prevVelocity + accelDir * accelVel;
    }


    static public Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity, MovementParameters parameters)
    {
        // Apply Friction
        float speed = prevVelocity.magnitude;
        if (speed != 0) // To avoid divide by zero errors
        {
            float drop = speed * parameters.friction * Time.fixedDeltaTime;
            prevVelocity *= Mathf.Max(speed - drop, 0) / speed; // Scale the velocity based on friction.
        }

        // ground_accelerate and max_velocity_ground are server-defined movement variables
        return MovementAfterAcceleration(accelDir, prevVelocity, parameters.ground_accelerate, parameters.max_velocity_ground);
    }

    static public Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity,  MovementParameters parameters)
    {
        // air_accelerate and max_velocity_air are server-defined movement variables
        return MovementAfterAcceleration(accelDir, prevVelocity, parameters.air_accelerate, parameters.max_velocity_air);
    }
}