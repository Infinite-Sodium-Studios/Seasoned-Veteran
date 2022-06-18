using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    bool initialized = false;
    private float speed;

    public void Init(float _speed)
    {
        initialized = true;
        speed = _speed;
    }

    public void StartMovement()
    {
        Debug.Assert(initialized);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        var position = ray.origin;
        var rotation = Camera.main.transform.rotation;
        gameObject.transform.SetPositionAndRotation(position, rotation);

        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(speed * rigidBody.transform.forward);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile " + gameObject + " just hit " + collision.gameObject);
        if (collision.gameObject.TryGetComponent<IHittable>(out var hittable))
        {
            hittable.HitEvent(gameObject);
        }
        Destroy(gameObject);
    }
}
