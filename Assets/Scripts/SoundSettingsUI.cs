using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    public GameObject panel;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    public void OpenPanel()
    {
        bool musicOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

        musicToggle.SetIsOnWithoutNotify(musicOn);
        sfxToggle.SetIsOnWithoutNotify(sfxOn);

        panel.SetActive(true);
    }

    public void ClosePanel() => panel.SetActive(false);

    public void OnMusicToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("MusicEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
        AudioManager.Instance.ApplyMusicSetting(isOn);  // Call proper method
    }

    public void OnSfxToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("SFXEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
        AudioManager.Instance.ApplySfxSetting(isOn);
    }
}