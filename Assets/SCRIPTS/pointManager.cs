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
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= GetRequiredScore())
        {
            GameManager.Instance.WinLevel();
        }
    }

    private void UpdateScoreUI()
    {
        int required = GetRequiredScore();
        scoreText.text = $"Score: {score}/{required}";
    }

    private int GetRequiredScore()
    {
        return (GameManager.Instance != null) ? GameManager.Instance.requiredScore : 10;
    }
}