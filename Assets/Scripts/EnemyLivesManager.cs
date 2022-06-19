using UnityEngine;

public class EnemyLivesManager : MonoBehaviour, IHittable
{
    [SerializeField] private int initialHealth;
    private EnemyTypeManager enemyTypeManager;
    private PlayerScoreManager playerScoreManager;

    private EnemySpawning spawner;
    private int currentHealth;
    void Start()
    {
        Debug.Assert(initialHealth > 0);
        currentHealth = initialHealth;
        spawner = GameObject.Find("EnemySpawnObject").GetComponent<EnemySpawning>();
        enemyTypeManager = gameObject.GetComponent<EnemyTypeManager>();
        playerScoreManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScoreManager>();
        Debug.Assert(enemyTypeManager != null, "All enemies should have a type");
    }

    public void HitEvent(GameObject hitter, WeaponStats weaponStats)
    {
        if (hitter.tag == "Enemy")
        {
            return;
        }
        Debug.Assert(enemyTypeManager != null, "All enemies should have a type");
        int typeOfEnemy = enemyTypeManager.enemyType;
        if (weaponStats.IsCompatibleWithEnemy(typeOfEnemy))
        {
            currentHealth--;
            if (currentHealth <= 0)
            {
                Debug.Log("Destroyed by " + hitter.name);
                playerScoreManager.OnEnemyKill();
                Destroy(gameObject);
            }
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
