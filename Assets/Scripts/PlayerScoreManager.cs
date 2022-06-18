using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    private int currentScore;
    private float timeSurvived;
    private float startTime;

    void Start()
    {
        currentScore = 0;
        timeSurvived = 0;
        startTime = Time.time;
    }

    public void OnEnemyKill()
    {
        Debug.Log("OnEnemyKill: " + currentScore);
        currentScore++;
        Debug.Log("Newscore " + currentScore);
    }

    public int GetScore()
    {
        return currentScore;
    }

    public float GetTimeSurvived()
    {
        return timeSurvived;
    }

    void Update()
    {
        timeSurvived = Time.time - startTime;
    }
}
