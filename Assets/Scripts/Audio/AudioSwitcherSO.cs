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
    private Dictionary<AudioTypeSO, List<AudioSource>> sourcesByAudioType;

    void OnEnable()
    {
        sourcesByAudioType = new Dictionary<AudioTypeSO, List<AudioSource>>();
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
        source.clip = matchingClip.audioClip;
        source.loop = !justOnce;
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