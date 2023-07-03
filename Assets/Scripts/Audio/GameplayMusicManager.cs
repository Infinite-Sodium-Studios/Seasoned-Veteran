using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameplayMusicManager : MonoBehaviour
{

    [SerializeField] private AudioSwitcherSO audioSwitcher;
    [SerializeField] private AudioTypeSO gameplayAudioType;
    private AudioSource source;
    private static bool isCreated = false;

    void Awake()
    {
        if (isCreated)
        {
            Destroy(gameObject);
            return;
        }
        isCreated = true;
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void ToggleMusicOnPause(bool isPaused)
    {
        if (!isPaused)
        {
            audioSwitcher.ResumeAudioClip(gameplayAudioType);
        }
        else
        {
            audioSwitcher.PauseAudioClip(gameplayAudioType);
        }
    }

    void PlayMusic()
    {
        if (source.isPlaying)
        {
            return;
        }
        audioSwitcher.PlayAudioClipLooped(gameplayAudioType, source);
        Debug.Assert(source.isPlaying);
    }
}
