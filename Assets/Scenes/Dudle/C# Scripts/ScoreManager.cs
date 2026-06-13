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
        Instance = this;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        // Проверка Player
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (player != null)
        {
            startY = player.position.y;
            highestY = startY;
        }

        // Скрываем GameOverPanel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Обновляем текст
        if (scoreText != null) scoreText.text = "Счёт: 0";
        if (highScoreText != null) highScoreText.text = "Рекорд: " + highScore;

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isGameOver) return;
        if (player == null) return;

        // Счёт по высоте
        if (player.position.y > highestY)
        {
            highestY = player.position.y;
            score = Mathf.FloorToInt((highestY - startY) * 10);

            if (scoreText != null)
                scoreText.text = "Счёт: " + score;

            if (highScoreText != null)
                highScoreText.text = "Рекорд: " + highScore;
        }

        // Проверка падения
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

        // Скрываем игровой счёт
        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (highScoreText != null) highScoreText.gameObject.SetActive(false);

        // Показываем панель Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null) finalScoreText.text = "Счёт: " + score;
            if (finalHighScoreText != null) finalHighScoreText.text = "Рекорд: " + highScore;
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        

SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}