public class PlayerScore
{
    public int score { get; private set; }
    public float timeSurvivedMs { get; private set; }
    public int enemiesEscaped { get; private set; }
    public int maxAllowedEscapedEnemies { get; private set; }

    public PlayerScore(int _maxAllowedEscapedEnemies)
    {
        score = 0;
        timeSurvivedMs = 0;
        enemiesEscaped = 0;
        maxAllowedEscapedEnemies = _maxAllowedEscapedEnemies;
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

    public void TickMs(float deltaTimeMs)
    {
        timeSurvivedMs += deltaTimeMs;
    }
}