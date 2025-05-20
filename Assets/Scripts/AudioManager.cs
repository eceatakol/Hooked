using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Sound Effects")]
    public AudioClip catchSFX;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load player preferences
            bool musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
            bool sfxEnabled = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

            ApplyMusicSetting(musicEnabled);
            ApplySfxSetting(sfxEnabled);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called when the Music toggle changes
        public void ApplyMusicSetting(bool enabled)
    {
        if (musicSource == null) return;

        musicSource.loop = true; // Make sure it stays looping

        if (enabled)
        {
            if (!musicSource.isPlaying && musicSource.clip != null)
            {
                musicSource.Play(); // Resume or restart playback
            }
        }
        else
        {
            musicSource.Stop(); // Stop playback
        }
    }

    // Called when the SFX toggle changes
    public void ApplySfxSetting(bool enabled)
    {
        if (sfxSource == null) return;
        sfxSource.mute = !enabled; // Preferred to .enabled = false for safety
    }

    // Called when fish is caught
    public void PlayCatchSound()
    {
        PlaySFX(catchSFX);
    }

    // General-purpose SFX play
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null && !sfxSource.mute)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}