using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    public float sensitivity;
    private static float defaultSensitivity = 0.5f;
    private static string sensitivityPrefName = "MouseSensitivity";

    private void Start() {
        sensitivity = PlayerPrefs.GetFloat(sensitivityPrefName, defaultSensitivity);
        Debug.Log("Loaded mouse sensitivity: " + sensitivity);
        SetMouseSensitivity(sensitivity);
    }

    public void SetMouseSensitivity(Slider sliderSensitivity) {
        SetMouseSensitivity(sliderSensitivity.value);
    }

    public void SetMouseSensitivity(float newSensitivity) {
        Debug.Log("Setting mouse sensitivity to: " + newSensitivity);
        PlayerPrefs.SetFloat(sensitivityPrefName, newSensitivity);
        sensitivity = newSensitivity;
    }

    public float GetMouseSensitivity() {
        return sensitivity;
    }
}
