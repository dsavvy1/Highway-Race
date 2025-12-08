using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // FIXED: Persist across scenes like GameManager
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        Debug.Log($"[ScoreManager] Added {amount}. Total: {score}/{GetRequiredScore()}"); // For debugging

        // Check for level win after each collection
        if (GameManager.Instance != null && score >= GameManager.Instance.requiredScore)
        {
            Debug.Log("[ScoreManager] Win condition met!");
            GameManager.Instance.WinLevel();
        }
    }

    private void UpdateScoreUI()
    {
        int required = GetRequiredScore();
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}/{required}";
        }
    }

    private int GetRequiredScore()
    {
        return (GameManager.Instance != null) ? GameManager.Instance.requiredScore : 10;
    }

    // NEW: Public reset for new levels
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
        Debug.Log("[ScoreManager] Score reset to 0 for new level.");
    }
}