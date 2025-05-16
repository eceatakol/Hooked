using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject startMenuCanvas;
    public GameObject inGameCanvas; // Your existing UI (Score Canvas)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional if you want GameManager to persist
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        startMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        Time.timeScale = 1f; // Make sure game is running
    }
}