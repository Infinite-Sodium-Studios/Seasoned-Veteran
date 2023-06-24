using UnityEngine;
using TMPro;

public class PlayerTimeSurvivedTextUpdate : MonoBehaviour
{
    [SerializeField] private PlayerScoreSO playerScore;
    [SerializeField] private string baseText;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        var timeSurvivedSec = playerScore.timeSurvivedMs / 1000f;
        string textValue = baseText + timeSurvivedSec.ToString("0.00");
        textElement.text = textValue;
    }
}
