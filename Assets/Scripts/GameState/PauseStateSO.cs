using UnityEngine;

[CreateAssetMenu(fileName = "PauseState", menuName = "Game State/Pause Manager")]
public class PauseStateSO : ScriptableObject
{
    [SerializeField] private GameEvent<bool> pauseEvent;
    [SerializeField] private GameEvent<string> actionMapEvent;
    private TriggerableAction<bool> pauseListener;

    private void OnEnable() {
        pauseListener = new TriggerableAction<bool>((isPaused) => {
            if (isPaused)
            {
                UnlockCursor();
                HaltTime();
                UseUIActionMap();
            }
            else
            {
                LockCursor();
                ResumeTime();
                UsePlayerActionMap();
            }
        });
        pauseEvent.Add(pauseListener);
    }

    private void OnDisable() {
        pauseEvent.Remove(pauseListener);
    }

    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void HaltTime()
    {
        Time.timeScale = 0f;
    }

    void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    void UseUIActionMap()
    {
        Debug.Assert(actionMapEvent != null, "actionMapEvent != null");
        actionMapEvent.Trigger("UI");
    }

    void UsePlayerActionMap()
    {
        actionMapEvent.Trigger("Player");
    }
}
