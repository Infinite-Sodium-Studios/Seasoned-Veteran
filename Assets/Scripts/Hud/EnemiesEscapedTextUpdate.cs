using UnityEngine;
using TMPro;

public class EnemiesEscapedTextUpdate : MonoBehaviour
{
    [SerializeField] private PlayerScoreManager playerScoreManager;
    [SerializeField] private string baseText;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        var enemiesEscapedSlack = Mathf.Max(0, playerScoreManager.GetEnemiesEscapedSlack());
        string textValue = baseText + enemiesEscapedSlack.ToString();
        textElement.text = textValue;
    }
}
