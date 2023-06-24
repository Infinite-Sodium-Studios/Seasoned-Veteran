using UnityEngine;
using TMPro;

public class PlayerScoreTextUpdate : MonoBehaviour
{
    [SerializeField] private PlayerScoreSO playerScore;
    [SerializeField] private string baseText;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        string textValue = baseText + playerScore.score.ToString();
        textElement.text = textValue;
    }
}
