using UnityEngine;

public class EnemyLivesManager : MonoBehaviour, IHittable
{
    [SerializeField] private int initialHealth;
    private PlayerScoreManager playerScoreManager;

    private EnemySpawning spawner;
    private int currentHealth;
    void Start()
    {
        Debug.Assert(initialHealth > 0);
        currentHealth = initialHealth;
        spawner = GameObject.Find("EnemySpawnObject").GetComponent<EnemySpawning>();
        playerScoreManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScoreManager>();
    }

    public void HitEvent(GameObject hitter)
    {
        if (hitter.tag == "Enemy")
        {
            return;
        }

        currentHealth--;
        if (currentHealth <= 0)
        {
            var randResult = Random.Range(0, 2);
            if (randResult == 0 || true)
            {
                Debug.Log("Destroyed by " + hitter.name);
                playerScoreManager.OnEnemyKill();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Doubled!");
                int enemyType = gameObject.GetComponent<EnemyTypeManager>().GetEnemyType();
                var collider = gameObject.GetComponent<Collider>();
                var sizeRight = 1.5f * collider.bounds.size.x;
                Vector3 spawnLocation = gameObject.transform.position + Vector3.right * sizeRight;
                spawner.Respawn(enemyType, new SpawnMotion(spawnLocation, gameObject.transform.forward));
            }
        }
    }
}
