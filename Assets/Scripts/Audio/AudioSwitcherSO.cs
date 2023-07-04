using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AudioReference
{
    public AudioClip  audioClip;
    public AudioTypeSO audioType;
}

[CreateAssetMenu(fileName = "AudioSwitcher", menuName = "Audio/Audio Switcher")]
public class AudioSwitcherSO: ScriptableObject
{
    [SerializeField] private List<AudioReference> audioClips;
    [SerializeField] private AudioVolumeSO audioVolumeSO;
    private Dictionary<AudioTypeSO, List<AudioSource>> sourcesByAudioType;
    private List<Action<AudioTypeSO>> onVolumeChangedListeners;

    void OnEnable()
    {
        sourcesByAudioType = new Dictionary<AudioTypeSO, List<AudioSource>>();
        onVolumeChangedListeners = new List<Action<AudioTypeSO>>();
    }

    void OnDisable()
    {
        foreach (var listener in onVolumeChangedListeners)
        {
            audioVolumeSO.OnVolumeChanged -= listener;
        }
    }

    AudioReference GetAudioClip(AudioTypeSO audioType)
    {
        var matchingClip = audioClips.Find((audioReference) => {
            return audioReference.audioType == audioType;
        });
        if (matchingClip == null) {
            Debug.LogWarning($"No audio clip found for {audioType}");
            return null;
        }
        return matchingClip;
    }

    void AddAudioSource(AudioTypeSO audioType, AudioSource source)
    {
        sourcesByAudioType[audioType] = sourcesByAudioType.GetValueOrDefault(audioType, new List<AudioSource>());
        sourcesByAudioType[audioType].Add(source);
    }

    void PlayAudioClip(AudioTypeSO audioType, bool justOnce, AudioSource source)
    {
        var matchingClip = GetAudioClip(audioType);
        if (matchingClip == null) {
            return;
        }

        var loaded = matchingClip.audioClip.LoadAudioData();
        if (!loaded)
        {
            Debug.LogWarning($"Failed to load audio clip for {audioType}");
            return;
        }
        AddAudioSource(audioType, source);
        Action<AudioTypeSO> updateSourceVolume = (AudioTypeSO changedAudioType) => {
            if (changedAudioType != audioType)
            {
                return;
            }
            source.volume = audioVolumeSO.GetVolume(audioType);
        };
        audioVolumeSO.OnVolumeChanged += updateSourceVolume;
        onVolumeChangedListeners.Add(updateSourceVolume);

        source.clip = matchingClip.audioClip;
        source.loop = !justOnce;
        updateSourceVolume(audioType);
        source.Play();
    }

    public void PlayAudioClipLooped(AudioTypeSO audioType, AudioSource source)
    {
        PlayAudioClip(audioType, false, source);
    }

    public void PlayAudioClipOnce(AudioTypeSO audioType, AudioSource source)
    {
        PlayAudioClip(audioType, true, source);
    }

    public void StopAudioClip(AudioTypeSO audioType, AudioSource source)
    {
        sourcesByAudioType[audioType].Remove(source);
        source.Stop();
    }

    public void PauseAudioClip(AudioTypeSO audioType)
    {
        if (!sourcesByAudioType.ContainsKey(audioType))
        {
            return;
        }
        foreach (var source in sourcesByAudioType[audioType])
        {
            source.Pause();
        }
    }

    public void ResumeAudioClip(AudioTypeSO audioType)
    {
        if (!sourcesByAudioType.ContainsKey(audioType))
        {
            return;
        }
        foreach (var source in sourcesByAudioType[audioType])
        {
            source.UnPause();
        }
    }
}