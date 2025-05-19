using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton
    public TextMeshProUGUI scoreText;

    public int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        Debug.Log("Updating score text");
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("scoreText is null!");
        }
    }
    public void ResetScore()
{
    score = 0;

    if (scoreText != null)
    {
        scoreText.text = "Score: 0";
    }
}
}