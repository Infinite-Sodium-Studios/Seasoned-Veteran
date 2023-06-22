using UnityEngine;

public class EnemyLivesManager : MonoBehaviour, IHittable
{
    [SerializeField] private int initialHealth;
    private PlayerScoreManager playerScoreManager;

    private int currentHealth;
    void Start()
    {
        Debug.Assert(initialHealth > 0);
        currentHealth = initialHealth;
        playerScoreManager = GameObject.Find("GameManagerObject").GetComponent<PlayerScoreManager>();
    }

    public void HitEvent(string hitter, WeaponStats weaponStats)
    {
        if (!weaponStats.CanHitEnemy(gameObject))
        {
            Debug.Log("Weapon could not damage enemy!");
            return;
        }

        currentHealth -= weaponStats.DamageToEnemy(gameObject);
        if (currentHealth <= 0)
        {
            Debug.Log("Destroyed by " + hitter);
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
