using UnityEngine;
using TMPro;

public class PlayerScoreTextUpdate : MonoBehaviour
{
    [SerializeField] private PlayerScoreManager playerScoreManager;
    [SerializeField] private string baseText;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        string textValue = baseText + playerScoreManager.GetScore().ToString();
        textElement.text = textValue;
    }
}
