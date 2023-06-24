using UnityEngine;
using TMPro;

public class EnemiesEscapedTextUpdate : MonoBehaviour
{
    [SerializeField] private PlayerScoreSO playerScore;
    [SerializeField] private string baseText;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        var enemiesEscapedSlack = Mathf.Max(0, playerScore.RemainingLives());
        string textValue = baseText + enemiesEscapedSlack.ToString();
        textElement.text = textValue;
    }
}
