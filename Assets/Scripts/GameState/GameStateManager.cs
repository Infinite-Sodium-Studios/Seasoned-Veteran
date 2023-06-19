using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    private bool gameEnded;
    void Start()
    {
        gameEnded = false;
        gameOverCanvas.SetActive(false);
    }

    public void GameOver()
    {
        if (gameEnded)
        {
            return;
        }
        gameEnded = true;
        gameOverCanvas.SetActive(true);
        Invoke("RestartGame", 5f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
