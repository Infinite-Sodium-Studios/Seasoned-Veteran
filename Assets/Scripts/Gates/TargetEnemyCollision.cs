using UnityEngine;

public class TargetEnemyCollision : MonoBehaviour
{
    [SerializeField] private PlayerScoreSO playerScore;
    private GameStateManager gameManager;

    void Start()
    {
        var gameManagerObject = GameObject.Find("GameManagerObject");
        gameManager = gameManagerObject.GetComponent<GameStateManager>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        Destroy(collider.gameObject);
        playerScore.OnEnemyEscape();
        if (playerScore.RemainingLives() <= 0)
        {
            gameManager.GameOver();
        }
    }
}
