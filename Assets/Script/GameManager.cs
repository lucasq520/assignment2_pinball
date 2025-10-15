using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Stats")]
    public int lives = 3;
    public int score = 0;

    [Header("UI References")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Transform spawnPoint;

    private bool isGameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateUI();
        SpawnBall();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (isGameOver)
            return;

        lives--;
        UpdateUI();

        if (lives > 0)
        {
            Invoke(nameof(SpawnBall), 0.5f);
        }
        else
        {
            GameOver();
        }
    }

    public void SpawnBall()
    {
        if (isGameOver) return;

        if (ballPrefab != null && spawnPoint != null)
        {
            Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    void UpdateUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives;

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void GameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
