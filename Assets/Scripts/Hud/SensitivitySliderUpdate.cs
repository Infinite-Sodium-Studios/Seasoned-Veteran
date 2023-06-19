using UnityEngine;
using UnityEngine.UI;

public class SensitivitySliderUpdate : MonoBehaviour
{
    [SerializeField] private SensitivityManager sensitivityManager;
    [SerializeField] private Slider sliderElement;

    void Update()
    {
        float sensitivityValue = sensitivityManager.GetMouseSensitivity();
        sliderElement.value = sensitivityValue;
    }
}
