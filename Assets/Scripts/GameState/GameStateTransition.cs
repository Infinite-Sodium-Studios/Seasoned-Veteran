using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateTransition
{
    public static void LoadScene(int sceneIndex)
    {
        if (sceneIndex == Constants.reloadCurrentLevelIndex) {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        SceneManager.LoadScene(sceneIndex);
    }

    public static void Quit()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}