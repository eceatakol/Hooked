using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    // Called when opening the panel
    public void OpenPanel()
    {
        if (panel == null || musicToggle == null || sfxToggle == null)
        {
            Debug.LogWarning("SoundSettingsUI is missing references.");
            return;
        }

        // Load saved preferences
        bool musicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

        // Update toggle visuals without triggering change events
        musicToggle.SetIsOnWithoutNotify(musicOn);
        sfxToggle.SetIsOnWithoutNotify(sfxOn);

        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void OnMusicToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (AudioManager.Instance != null)
            AudioManager.Instance.ApplyMusicSetting(isOn);
    }

    public void OnSfxToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (AudioManager.Instance != null)
            AudioManager.Instance.ApplySfxSetting(isOn);
    }
}