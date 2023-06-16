using UnityEngine;

public class EnemyTargetCollision : MonoBehaviour
{
    private PlayerScoreManager playerScoreManager;
    [SerializeField] private GameStateManager gameManager;

    void Start()
    {
        playerScoreManager = GameObject.Find("GameManagerObject").GetComponent<PlayerScoreManager>();
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
