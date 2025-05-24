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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ApplyMusicSetting(bool enabled)
    {
        Debug.Log("ApplyMusicSetting: " + enabled);

        if (musicSource == null)
        {
            Debug.LogError("❌ musicSource not assigned!");
            return;
        }

        if (musicSource.clip == null)
        {
            Debug.LogError("❌ musicSource.clip is null!");
            return;
        }

        musicSource.loop = true;

        if (enabled)
        {
            // Zaten oynuyorsa yeniden başlatma
            if (!musicSource.isPlaying)
            {
                musicSource.Play();
                Debug.Log("🎵 Music PLAYED");
            }
        }
        else
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
                Debug.Log("🔇 Music STOPPED");
            }
        }
    }

    public void ApplySfxSetting(bool enabled)
    {
        if (sfxSource != null)
        {
            sfxSource.mute = !enabled;
        }
    }

    public void PlayCatchSound()
    {
        PlaySFX(catchSFX);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null && !sfxSource.mute)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    
    private void Update()
{
    if (Input.GetKeyDown(KeyCode.M))
    {
        Debug.Log("🎛️ M key pressed: Toggling music");
        bool current = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        bool next = !current;
        PlayerPrefs.SetInt("MusicEnabled", next ? 1 : 0);
        PlayerPrefs.Save();

        ApplyMusicSetting(next);
    }
}
}