using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 targetVelocity;
    readonly private float speed = 3.0f;

    static Vector3 WithoutY(Vector3 vec)
    {
        var res = vec;
        res.y = 0;
        return res;
    }

    public void Initialize(Vector3 initialDirection)
    {
        targetVelocity = WithoutY(initialDirection).normalized * speed;
    }

    void FixedUpdate()
    {
        var rigidBody = GetComponent<Rigidbody>();
        var currentVelocity = rigidBody.velocity;
        rigidBody.AddForce(targetVelocity - WithoutY(currentVelocity), ForceMode.VelocityChange);
    }
}
