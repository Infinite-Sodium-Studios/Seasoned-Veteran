using System.Collections;
using UnityEngine;

public class TargetEnemyCollision : MonoBehaviour
{
    [SerializeField] private PlayerScoreSO playerScoreSO;
    [SerializeField] private GameStateSO gameStateSO;
    private PlayerScore playerScore;

    void Start()
    {
        playerScore = playerScoreSO.playerScore;
    }

    IEnumerator OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Enemy"))
        {
            yield return null;
        }
        if (collider.gameObject == null) {
            // if we destroy the enemy right as it hits the target
            // favor the player and ignore the collision
            yield return null;
        }
        Destroy(collider.gameObject);
        playerScore.OnEnemyEscape();
        if (playerScore.RemainingLives() <= 0)
        {
            StartCoroutine(gameStateSO.GameOverThenRestartAfterDelayMs(5_000));
        }
    }
}
