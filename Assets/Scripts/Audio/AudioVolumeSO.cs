using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
class AudioVolumeSetting
{
    public float defaultVolume;
    public AudioTypeSO audioType;
}

[CreateAssetMenu(fileName = "AudioVolume", menuName = "Audio/Audio Volume")]
class AudioVolumeSO: ScriptableObject
{
    [SerializeField] private List<AudioVolumeSetting> volumeSettings;
    public Dictionary<AudioTypeSO, float> currentVolumes { get; private set; }
    public event Action<AudioTypeSO> OnVolumeChanged;

    void OnEnable()
    {
        currentVolumes = new Dictionary<AudioTypeSO, float>();
        foreach (var setting in volumeSettings)
        {
            var audioTypeName = setting.audioType.name;
            var volume = PlayerPrefs.GetFloat(audioTypeName, setting.defaultVolume);
            Debug.Log("Loaded volume of type " + audioTypeName + " to " + volume);
            SetVolume(setting.audioType, volume);
        }
    }

    public void SetVolume(AudioTypeSO audioType, float volume)
    {        
        OnVolumeChanged?.Invoke(audioType);
        
        var audioTypeName = audioType.name;
        currentVolumes[audioType] = volume;
        PlayerPrefs.SetFloat(audioTypeName, volume);
        Debug.Log("Saved volume of type " + audioTypeName + " to " + volume);
    }

    public float GetVolume(AudioTypeSO audioType)
    {
        return currentVolumes.GetValueOrDefault(audioType, 1.0f);
    }
}