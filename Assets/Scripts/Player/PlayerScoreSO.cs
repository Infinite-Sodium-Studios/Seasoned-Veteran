using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScore", menuName = "Player Score", order = 1)]
public class PlayerScoreSO : Tickable
{
    public int score { get; private set; }
    public float timeSurvivedMs { get; private set; }
    private int enemiesEscaped;
    [SerializeField] private int maxAllowedEscapedEnemies;

    void OnEnable()
    {
        score = 0;
        timeSurvivedMs = 0;
        enemiesEscaped = 0;
    }

    public void OnEnemyKill()
    {
        score++;
    }

    public void OnEnemyEscape()
    {
        enemiesEscaped++;
    }

    public int RemainingLives()
    {
        return maxAllowedEscapedEnemies - enemiesEscaped;
    }

    public override void TickMs(float deltaTimeMs)
    {
        timeSurvivedMs += deltaTimeMs;
    }
}
