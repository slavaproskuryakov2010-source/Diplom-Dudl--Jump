using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Игрок")]
    [SerializeField] private Transform player;
    [SerializeField] private float fallDeathY = -10f;

    [Header("UI - Игровой экран")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("UI - Панель Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalHighScoreText;

    private int score;
    private int highScore;
    private float startY;
    private float highestY;
    private bool isGameOver;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        if (player != null)
        {
            startY = player.position.y;
            highestY = startY;
        }

        if (scoreText != null)
            scoreText.gameObject.SetActive(true);
        if (highScoreText != null)
            highScoreText.gameObject.SetActive(true);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateUI();
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isGameOver) return;

        if (player != null)
        {
            if (player.position.y > highestY)
            {
                highestY = player.position.y;
                score = Mathf.FloorToInt((highestY - startY) * 10);
                UpdateUI();
            }

            Camera cam = Camera.main;
            if (cam != null)
            {
                float deathY = cam.transform.position.y - cam.orthographicSize + fallDeathY;
                if (player.position.y < deathY)
                {
                    GameOver();
                }
            }
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Счёт: " + score;

        if (highScoreText != null)
            highScoreText.text = "Рекорд: " + highScore;
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (scoreText != null)
            scoreText.gameObject.SetActive(false);
        if (highScoreText != null)
            highScoreText.gameObject.SetActive(false);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (finalScoreText != null)
                finalScoreText.text = "Счёт: " + score;

            if (finalHighScoreText != null)
                finalHighScoreText.text = "Рекорд: " + highScore;
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}