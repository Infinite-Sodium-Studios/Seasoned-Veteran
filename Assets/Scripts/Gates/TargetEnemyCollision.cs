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
        Destroy(collider.gameObject);
        playerScore.OnEnemyEscape();
        if (playerScore.RemainingLives() <= 0)
        {
            StartCoroutine(gameStateSO.GameOverThenRestartAfterDelayMs(5_000));
        }
    }
}
