using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameEvent<bool> pauseEvent;

    private TriggerableAction<bool> pauseListener;


    private void Awake()
    {
        pauseListener = new TriggerableAction<bool>((isPaused) => {
            if (isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        });
    }

    private void OnEnable() {
        pauseEvent.Add(pauseListener);
    }

    private void OnDisable() {
        pauseEvent.Remove(pauseListener);
    }

    public void Pause() {
        Debug.Log("Pausing");
    }

    public void Resume() {
        Debug.Log("Resuming");
        playerInput.SwitchCurrentActionMap("Player");
    }
}
