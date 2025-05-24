using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private void Start()
    {
        // Oyun başladığında toggle'lar doğru pozisyonda mı diye kontrol etmek istersen
        if (musicToggle != null)
            musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);

        if (sfxToggle != null)
            sfxToggle.onValueChanged.AddListener(OnSfxToggleChanged);
    }

    public void OpenPanel()
    {
        if (panel == null || musicToggle == null || sfxToggle == null)
        {
            Debug.LogWarning("🎧 SoundSettingsUI: Eksik referans var.");
            return;
        }

        // PlayerPrefs'ten son ayarları al ve toggle'lara uygula
        bool musicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

        musicToggle.SetIsOnWithoutNotify(musicOn);
        sfxToggle.SetIsOnWithoutNotify(sfxOn);

        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel?.SetActive(false);
    }

    public void OnMusicToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();

        AudioManager.Instance?.ApplyMusicSetting(isOn);
    }

    public void OnSfxToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();

        AudioManager.Instance?.ApplySfxSetting(isOn);
    }
}