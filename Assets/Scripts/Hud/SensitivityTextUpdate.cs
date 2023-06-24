using UnityEngine;
using TMPro;

public class SensitivityTextUpdate : MonoBehaviour
{
    [SerializeField] private MouseSensitivitySO sensitivity;
    [SerializeField] private TextMeshProUGUI textElement;

    void Update()
    {
        string textValue = sensitivity.GetMouseSensitivity().ToString("0.00");
        textElement.text = textValue;
    }
}
