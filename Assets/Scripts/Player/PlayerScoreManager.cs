using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    private int currentScore;
    private float timeSurvived;
    private float startTime;
    private int enemiesEscaped;
    [SerializeField] private int maxAllowedEscapedEnemies;

    void Start()
    {
        currentScore = 0;
        timeSurvived = 0;
        startTime = Time.time;
        enemiesEscaped = 0;
    }

    public void OnEnemyKill()
    {
        currentScore++;
    }

    public void OnEnemyEscape()
    {
        enemiesEscaped++;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public float GetTimeSurvived()
    {
        return timeSurvived;
    }

    public int GetEnemiesEscapedSlack()
    {
        return maxAllowedEscapedEnemies - enemiesEscaped;
    }

    void Update()
    {
        timeSurvived = Time.time - startTime;
    }
}
