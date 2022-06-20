using UnityEngine;

public class CausesHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IHittable hittable))
        {
            hittable.HitEvent(gameObject, new WeaponStats());
        }
    }
}
