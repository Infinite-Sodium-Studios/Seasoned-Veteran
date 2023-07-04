using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioVolumeUIUpdate : MonoBehaviour
{
    [SerializeField] private AudioTypeSO audioType;
    [SerializeField] private AudioVolumeSO volumeSO;
    [SerializeField] private TextMeshProUGUI textElement;
    [SerializeField] private Slider sliderElement;

    void Start()
    {
        var volume = GetVolume();
        sliderElement.value = volume;
    }

    void Update()
    {
        MatchVolumeToSlider();
        UpdateText();
    }

    float GetVolume()
    {
        return volumeSO.GetVolume(audioType);
    }

    void MatchVolumeToSlider()
    {
        var volume = sliderElement.value;
        volumeSO.SetVolume(audioType, volume);
    }

    void UpdateText()
    {
        var volume = GetVolume();
        string textValue = volume.ToString("0.000");
        textElement.text = textValue;
    }
}
