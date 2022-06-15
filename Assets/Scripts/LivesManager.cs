using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private GameStateManager gameManager;
    [SerializeField] private int initialLives;
    private int currentLives;
    void Start()
    {
        Debug.Assert(initialLives > 0);
        currentLives = initialLives;
    }

    public int GetLives()
    {
        return Mathf.Max(0, currentLives);
    }

    // Update is called once per frame
    public void HitEvent()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            gameManager.GameOver();
        }
    }
}
