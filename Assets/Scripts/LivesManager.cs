using UnityEngine;

public class LivesManager : MonoBehaviour, IHittable
{
    [SerializeField] private GameStateManager gameManager;
    [SerializeField] private int initialLives;
    private int currentLives;
    void Start()
    {
        Debug.Assert(initialLives > 0);
        currentLives = initialLives;
    }

    public int GetLives()
    {
        return Mathf.Max(0, currentLives);
    }

    public void HitEvent(GameObject hitter, WeaponStats weaponStats)
    {
        currentLives--;
        if (currentLives <= 0)
        {
            gameManager.GameOver();
        }
    }
}
