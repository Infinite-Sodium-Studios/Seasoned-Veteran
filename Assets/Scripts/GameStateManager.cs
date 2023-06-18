using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverTextObject;
    private bool gameEnded;
    void Start()
    {
        gameEnded = false;
        gameOverTextObject.SetActive(false);
        Cursor.visible = false;
    }

    public void GameOver()
    {
        if (gameEnded)
        {
            return;
        }
        gameEnded = true;
        gameOverTextObject.SetActive(true);
        Invoke("RestartGame", 5f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
