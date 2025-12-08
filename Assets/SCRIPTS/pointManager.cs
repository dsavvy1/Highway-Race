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

        // Check for level win after each collection
        if (GameManager.Instance != null && score >= GameManager.Instance.requiredScore)
        {
            GameManager.Instance.WinLevel();
        }
    }

    private void UpdateScoreUI()
    {
        int required = (GameManager.Instance != null) ? GameManager.Instance.requiredScore : 10;
        scoreText.text = $"Score: {score}/{required}";
    }
}