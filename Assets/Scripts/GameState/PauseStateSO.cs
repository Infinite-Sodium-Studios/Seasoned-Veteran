using UnityEngine;
using StarterAssets;

[CreateAssetMenu(fileName = "PauseState", menuName = "Game State/Pause Manager", order = 1)]
public class PauseState : ScriptableObject
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private StarterAssetsInputs inputs;
    private bool isPaused;
    private PauseAction action;

    private void Awake()
    {
        action = new PauseAction();
    }

    private void OnEnable() {
        action.Enable();
    }

    private void OnDisable() {
        action.Disable();
    }

    private void Start() {
        Resume();
        action.Pause.PauseGame.performed += _ => {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        };
    }

    public void Pause() {
        Debug.Log("Pausing");
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inputs.Disable();
        isPaused = true;
    }

    public void Resume() {
        Debug.Log("Resuming");
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inputs.Enable();
        isPaused = false;
    }

    public bool IsPaused() {
        return isPaused;
    }
}
