using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MouseSensitivity", menuName = "Mouse Sensitivity", order = 1)]
public class MouseSensitivitySO : ScriptableObject
{
    public float sensitivity;
    [SerializeField] private static float defaultSensitivity = 0.5f;
    private static string sensitivityPrefName = "MouseSensitivity";

    private void Awake() {
        sensitivity = PlayerPrefs.GetFloat(sensitivityPrefName, defaultSensitivity);
        Debug.Log("Loaded mouse sensitivity: " + sensitivity);
        SetMouseSensitivity(sensitivity);
    }

    public void MatchSliderSensitivity(Slider sliderSensitivity) {
        SetMouseSensitivity(sliderSensitivity.value);
    }

    public void SetMouseSensitivity(float newSensitivity) {
        PlayerPrefs.SetFloat(sensitivityPrefName, newSensitivity);
        sensitivity = newSensitivity;
    }

    public float GetMouseSensitivity() {
        return sensitivity;
    }
}
