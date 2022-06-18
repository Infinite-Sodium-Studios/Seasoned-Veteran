using UnityEngine;

public class CausesHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CausesHit object just hit " + collision.collider.gameObject);
        if (collision.gameObject.TryGetComponent(out IHittable hittable))
        {
            Debug.Log(gameObject + " has CausesHit and did damage");
            hittable.HitEvent(gameObject);
        }
    }
}
