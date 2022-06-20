using UnityEngine;
using TMPro;

public class PlayerTimeSurvivedTextUpdate : MonoBehaviour
{
    [SerializeField] private PlayerScoreManager playerScoreManager;
    [SerializeField] private string baseText;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        string textValue = baseText + playerScoreManager.GetTimeSurvived().ToString("0.00");
        textElement.text = textValue;
    }
}
