using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out LivesManager livesManager))
        {
            livesManager.HitEvent();
            Destroy(gameObject);
        }
    }
}
