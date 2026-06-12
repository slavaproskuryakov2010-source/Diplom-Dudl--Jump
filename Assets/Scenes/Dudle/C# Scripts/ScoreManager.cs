using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text finalHighScoreText;

    [Header("Настройки")]
    [SerializeField] private Transform player;
    [SerializeField] private float fallDeathY = -10f;  // Смерть при падении ниже этой точки относительно камеры

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

        // Загружаем рекорд
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        if (player != null)
        {
            startY = player.position.y;
            highestY = startY;
        }

        UpdateUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver) return;

        if (player != null)
        {
            // Обновляем максимальную высоту
            if (player.position.y > highestY)
            {
                highestY = player.position.y;
                score = Mathf.FloorToInt((highestY - startY) * 10);
                UpdateUI();
            }

            // Проверка падения ниже камеры
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

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Счёт: " + score;
        }

        if (highScoreText != null)
        {
            highScoreText.text = "Рекорд: " + highScore;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // Сохраняем рекорд
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // Показываем панель
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            if (finalScoreText != null)
            {
                finalScoreText.text = "Счёт: " + score;
            }

            if (finalHighScoreText != null)
            {
                finalHighScoreText.text = "Рекорд: " + highScore;
            }
        }

        // Останавливаем время
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Если есть сцена меню
        // Или закройте игру:
        // Application.Quit();
    }
}