using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Game State/Game State Manager")]
public class GameStateSO: ScriptableObject
{
    [SerializeField] private IntGameEvent loadSceneEvent;
    [SerializeField] private VoidGameEvent restartGameEvent;
    [SerializeField] private BoolGameEvent gameOverEvent;
    [SerializeField] private VoidGameEvent quitGameEvent;
    private TriggerableAction<int> loadSceneListener;
    private VoidTriggerableAction quitGameListener;
    private TriggerableAction<bool> gameOverStateListener;
    private bool gameEnded;

    public void OnEnable()
    {
        gameEnded = false;
        loadSceneListener = new TriggerableAction<int>(GameStateTransition.LoadScene);
        quitGameListener = new VoidTriggerableAction(GameStateTransition.Quit);
        gameOverStateListener = new TriggerableAction<bool>((state) => gameEnded = state);
        loadSceneEvent.Add(loadSceneListener);
        quitGameEvent.Add(quitGameListener);
        gameOverEvent.Add(gameOverStateListener);
    }

    public void OnDisable()
    {
        loadSceneEvent.Remove(loadSceneListener);
        quitGameEvent.Remove(quitGameListener);
        gameOverEvent.Remove(gameOverStateListener);
    }

    public void GameOver()
    {
        if (gameEnded)
        {
            return;
        }
        gameOverEvent.Trigger(true);
    }

    public void RestartGame()
    {
        loadSceneEvent.Trigger(Constants.reloadCurrentLevelIndex);
        restartGameEvent.Trigger();
        gameOverEvent.Trigger(false);
    }
    
    public IEnumerator GameOverThenRestartAfterDelayMs(float delayMs)
    {
        GameOver();
        yield return new WaitForSeconds(delayMs / 1000f);
        RestartGame();
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        quitGameEvent.Trigger();
    }
}