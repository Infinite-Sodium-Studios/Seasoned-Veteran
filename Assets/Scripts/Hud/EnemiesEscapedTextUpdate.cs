using UnityEngine;
using TMPro;

public class EnemiesEscapedTextUpdate : MonoBehaviour
{
    [SerializeField] private PlayerScoreSO playerScoreSO;
    [SerializeField] private string baseText;
    [SerializeField] private TextMeshProUGUI textElement;
    private PlayerScore playerScore;

    void Start()
    {
        playerScore = playerScoreSO.playerScore;
    }

    void Update()
    {
        var enemiesEscapedSlack = Mathf.Max(0, playerScore.RemainingLives());
        string textValue = baseText + enemiesEscapedSlack.ToString();
        textElement.text = textValue;
    }
}
