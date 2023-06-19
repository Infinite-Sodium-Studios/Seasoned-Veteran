using UnityEngine;
using TMPro;

public class SensitivityTextUpdate : MonoBehaviour
{
    [SerializeField] private SensitivityManager sensitivityManager;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        string textValue = sensitivityManager.GetMouseSensitivity().ToString("0.00");
        textElement.text = textValue;
    }
}
