using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScore", menuName = "Player Score", order = 1)]
public class PlayerScoreSO : Tickable
{
    public PlayerScore playerScore { get; private set; }
    [SerializeField] private int numLives;
    [SerializeField] private VoidGameEvent restartGameEvent;
    private VoidTriggerableAction restartGameListener;

    void Init()
    {
        playerScore = new PlayerScore(numLives);
    }

    public void OnEnable()
    {
        Init();
        restartGameListener = new VoidTriggerableAction(Init);
        restartGameEvent.Add(restartGameListener);
    }

    public void OnDisable()
    {
        restartGameEvent.Remove(restartGameListener);
    }

    public override void TickMs(float deltaTimeMs)
    {
        playerScore.TickMs(deltaTimeMs);
    }
}
