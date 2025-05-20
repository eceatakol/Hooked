using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip catchSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Read prefs at startup
            bool musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
            bool sfxEnabled = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

            //ApplyMusicSetting(musicEnabled);
            //ApplySfxSetting(sfxEnabled);
             ApplyMusicSetting(PlayerPrefs.GetInt("MusicEnabled", 1) == 1);
             ApplySfxSetting(PlayerPrefs.GetInt("SFXEnabled", 1) == 1);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyMusicSetting(bool enabled)
    {
        if (musicSource == null) return;

        if (enabled)
        {
            if (!musicSource.isPlaying && musicSource.clip != null)
            {
                musicSource.loop = true;
                musicSource.Play();
            }
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void ApplySfxSetting(bool enabled)
    {
        if (sfxSource == null) return;
        sfxSource.enabled = enabled;
    }

    public void PlayCatchSound()
    {
        PlaySFX(catchSFX);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource.enabled && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}