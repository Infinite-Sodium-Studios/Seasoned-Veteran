using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverTextObject;
    private bool gameEnded;
    void Start()
    {
        Init();
    }

    void Init()
    {
        gameEnded = false;
        gameOverTextObject.SetActive(false);
    }

    public void GameOver()
    {
        if (gameEnded)
        {
            return;
        }
        gameEnded = true;
        Debug.Log("Game over!");
        LoadGameOverScene();
        Invoke("RestartGame", 5f);
        ResumeTime();
    }

    void LoadGameOverScene()
    {
        Debug.Log("Overlay should pop up");
        gameOverTextObject.SetActive(true);
        Debug.Log("IsActive: " + gameOverTextObject.activeSelf);
    }

    void PauseTime()
    {
        Time.timeScale = 0f;
    }

    void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
