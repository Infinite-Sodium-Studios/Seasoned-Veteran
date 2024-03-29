using UnityEngine;

public class EnemyLivesManager : MonoBehaviour, IHittable
{
    [SerializeField] private int initialHealth;
    private GameObject enemyObject;
    private PlayerScoreManager playerScoreManager;

    private EnemySpawning spawner;
    private int currentHealth;
    void Start()
    {
        Debug.Assert(initialHealth > 0);
        currentHealth = initialHealth;
        spawner = GameObject.Find("EnemySpawnObject").GetComponent<EnemySpawning>();
        enemyObject = gameObject;
        playerScoreManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScoreManager>();
    }

    public void HitEvent(GameObject hitter, WeaponStats weaponStats)
    {
        if (hitter.tag == "Enemy" || enemyObject == null)
        {
            return;
        }
        if (!weaponStats.CanHitEnemy(enemyObject))
        {
            Debug.Log("Weapon could not damage enemy!");
            return;
        }

        currentHealth -= weaponStats.DamageToEnemy(enemyObject);
        if (currentHealth <= 0)
        {
            Debug.Log("Destroyed by " + hitter.name);
            playerScoreManager.OnEnemyKill();
            Destroy(gameObject);
        }
    }

    public int InitialHealth()
    {
        return initialHealth;
    }

    public int CurrentHealth()
    {
        return currentHealth;
    }
}
