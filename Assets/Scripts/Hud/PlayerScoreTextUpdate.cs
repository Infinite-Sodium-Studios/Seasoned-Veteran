using UnityEngine;
using TMPro;

public class PlayerScoreTextUpdate : MonoBehaviour
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
        string textValue = baseText + playerScore.score.ToString();
        textElement.text = textValue;
    }
}
