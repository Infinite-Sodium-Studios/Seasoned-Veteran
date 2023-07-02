using UnityEngine;
using TMPro;

public class PlayerTimeSurvivedTextUpdate : MonoBehaviour
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
        var timeSurvivedSec = playerScore.timeSurvivedMs / 1000f;
        string textValue = baseText + timeSurvivedSec.ToString("0.00");
        textElement.text = textValue;
    }
}
