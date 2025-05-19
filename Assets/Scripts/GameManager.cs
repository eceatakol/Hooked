using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public CanvasGroup startMenuGroup;
    public GameObject startMenuCanvas;
    public GameObject inGameCanvas;
    public TextMeshProUGUI timerText;
    public GameObject endScreenCanvas;
    public TextMeshProUGUI finalScoreText;

    [Header("Game Settings")]
    public float gameDuration = 60f;

    private float timeRemaining;
    private bool timerRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startMenuCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
        endScreenCanvas.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        inGameCanvas.SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(FadeOutStartMenu());

        timeRemaining = gameDuration;
        timerRunning = true;
    }

    IEnumerator FadeOutStartMenu()
    {
        float duration = 1f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            startMenuGroup.alpha = alpha;
            yield return null;
        }

        startMenuGroup.alpha = 0f;
        startMenuGroup.interactable = false;
        startMenuGroup.blocksRaycasts = false;

        startMenuCanvas.SetActive(false);
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

    void UpdateTimerUI()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        if (timerText != null)
        {
            timerText.text = "Time: " + seconds;
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        endScreenCanvas.SetActive(true);

        if (finalScoreText != null && ScoreManager.Instance != null)
        {
            finalScoreText.text = "Your Score: " + ScoreManager.Instance.score;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}