using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool isRestarting = false;

    [Header("UI References")]
    public CanvasGroup startMenuGroup;
    public GameObject startMenuCanvas;
    public GameObject rulesPanel;
    public GameObject inGameCanvas;
    public TextMeshProUGUI timerText;
    public GameObject endScreenCanvas;
    public TextMeshProUGUI finalScoreText;

    [Header("Game Settings")]
    public float gameDuration = 60f;

    private float timeRemaining;
    private bool timerRunning = false;
    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        // ✅ Uygulama her açıldığında ses ayarlarını uygula
        ApplySoundSettings();

        if (PlayerPrefs.GetInt("Restarting", 0) == 1)
        {
            PlayerPrefs.DeleteKey("Restarting");

            startMenuCanvas?.SetActive(false);
            if (startMenuGroup != null)
            {
                startMenuGroup.alpha = 0f;
                startMenuGroup.interactable = false;
                startMenuGroup.blocksRaycasts = false;
            }

            inGameCanvas?.SetActive(true);
            endScreenCanvas?.SetActive(false);
            Time.timeScale = 1f;

            timeRemaining = gameDuration;
            timerRunning = true;

            EnableGameplayScripts();
        }
        else
        {
            startMenuCanvas?.SetActive(true);
            inGameCanvas?.SetActive(false);
            endScreenCanvas?.SetActive(false);
            Time.timeScale = 0f;

            DisableGameplayScripts();
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                EndGame();
            }
        }
    }

    private void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        if (timerText != null)
            timerText.text = "Time: " + seconds;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        inGameCanvas?.SetActive(true);

        timeRemaining = gameDuration;
        timerRunning = true;

        StartCoroutine(FadeOutStartMenu());

        EnableGameplayScripts();

        // ✅ Oyun başlarken ayarları uygula
        ApplySoundSettings();
    }

    private IEnumerator FadeOutStartMenu()
    {
        float duration = 1f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            if (startMenuGroup != null)
                startMenuGroup.alpha = alpha;

            yield return null;
        }

        if (startMenuGroup != null)
        {
            startMenuGroup.alpha = 0f;
            startMenuGroup.interactable = false;
            startMenuGroup.blocksRaycasts = false;
        }

        startMenuCanvas?.SetActive(false);
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        endScreenCanvas?.SetActive(true);

        if (finalScoreText != null && ScoreManager.Instance != null)
            finalScoreText.text = "Your Score: " + ScoreManager.Instance.score;
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("Restarting", 1);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 0f;

        endScreenCanvas?.SetActive(false);
        inGameCanvas?.SetActive(false);
        startMenuCanvas?.SetActive(true);

        if (startMenuGroup != null)
        {
            startMenuGroup.alpha = 1f;
            startMenuGroup.interactable = true;
            startMenuGroup.blocksRaycasts = true;
        }

        timeRemaining = gameDuration;
        timerRunning = false;
        UpdateTimerUI();

        ScoreManager.Instance?.ResetScore();

        foreach (var fish in GameObject.FindGameObjectsWithTag("Fish"))
            Destroy(fish);

        var boat = GameObject.Find("single boat");
        boat?.GetComponent<BoatResetter>()?.ResetBoat();

        DisableGameplayScripts();
        Time.timeScale = 1f;

        // ✅ Menüye dönünce de ses ayarlarını yeniden uygula
        ApplySoundSettings();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ShowRules()
    {
        rulesPanel?.SetActive(true);
    }

    public void HideRules()
    {
        rulesPanel?.SetActive(false);
    }

    private void DisableGameplayScripts()
{
    FishSpawner spawner = FindFirstObjectByType<FishSpawner>();
    if (spawner != null) spawner.enabled = false;

    BoatController boat = FindFirstObjectByType<BoatController>();
    if (boat != null) boat.enabled = false;
}

private void EnableGameplayScripts()
{
    FishSpawner spawner = FindFirstObjectByType<FishSpawner>();
    if (spawner != null) spawner.enabled = true;

    BoatController boat = FindFirstObjectByType<BoatController>();
    if (boat != null) boat.enabled = true;
}

    private void ApplySoundSettings()
    {
        if (AudioManager.Instance == null) return;

        bool musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        bool sfxEnabled = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;

        AudioManager.Instance.ApplyMusicSetting(musicEnabled);
        AudioManager.Instance.ApplySfxSetting(sfxEnabled);
    }
}