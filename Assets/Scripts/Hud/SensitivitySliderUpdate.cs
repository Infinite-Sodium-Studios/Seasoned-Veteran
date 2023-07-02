using UnityEngine;
using UnityEngine.UI;

public class SensitivitySliderUpdate : MonoBehaviour
{
    [SerializeField] private MouseSensitivitySO sensitivity;
    [SerializeField] private Slider sliderElement;

    void Start()
    {
        float sensitivityValue = sensitivity.GetMouseSensitivity();
        sliderElement.value = sensitivityValue;
    }

    void Update()
    {
        sensitivity.SetMouseSensitivity(sliderElement.value);
    }
}
