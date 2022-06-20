using UnityEngine;
using TMPro;

public class LivesTextUpdate : MonoBehaviour
{
    [SerializeField] private LivesManager livesManager;
    [SerializeField] private string baseText;
    [SerializeField] private TextMeshProUGUI textElement;

    // Update is called once per frame
    void Update()
    {
        string textValue = baseText + livesManager.GetLives().ToString();
        textElement.text = textValue;
    }
}
