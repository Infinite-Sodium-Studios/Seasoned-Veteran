using UnityEngine;

public class EnemyTargetCollision : MonoBehaviour
{
    private PlayerScoreManager playerScoreManager;
    private GameStateManager gameManager;

    void Start()
    {
        var gameManagerObject = GameObject.Find("GameManagerObject");
        gameManager = gameManagerObject.GetComponent<GameStateManager>();
        playerScoreManager = gameManagerObject.GetComponent<PlayerScoreManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        Destroy(collider.gameObject);
        Debug.Assert(playerScoreManager != null, "PlayerScoreManager cannot be null");
        playerScoreManager.OnEnemyEscape();
        if (playerScoreManager.GetEnemiesEscapedSlack() <= 0)
        {
            gameManager.GameOver();
        }
    }
}
