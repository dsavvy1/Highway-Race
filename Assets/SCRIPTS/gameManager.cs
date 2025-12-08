using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Level levelScript; // assign in inspector
    public ScoreManager scoreManager; // assign in inspector (optional now)
    public int requiredScore = 10;
    private bool endHandled = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void WinLevel()
    {
        if (endHandled)
        {
            Debug.Log("[GameManager] WinLevel called but end already handled.");
            return;
        }
        endHandled = true;
        Debug.Log("[GameManager] Collected enough petrol! -> Load Level 2");
        if (levelScript != null)
            levelScript.LoadGameScene2();
        else
            Debug.LogError("[GameManager] levelScript missing!");
    }

    public void PlayerDied()
    {
        if (endHandled)
        {
            Debug.Log("[GameManager] PlayerDied called but end already handled.");
            return;
        }
        endHandled = true;
        Debug.Log("[GameManager] Player died -> Load Game Over scene");
        if (levelScript != null)
            levelScript.LoadGameOverScene();
        else
            Debug.LogError("[GameManager] levelScript missing!");
    }

    public void AllPetrolFinished()
    {
        if (endHandled) return;

        int currentScore = ScoreManager.Instance != null ? ScoreManager.Instance.score : 0;
        Debug.Log($"[GameManager] All petrol finished. score={currentScore}, required={requiredScore}");

        if (currentScore >= requiredScore)
        {
            // Win already handled by AddScore(), but confirm here if needed
            WinLevel();
        }
        else
        {
            // FIXED: No early GameOver. Instead, respawn for another chance
            Debug.Log("[GameManager] Not enough score, but respawning petrol for another try...");
            // Find spawner and respawn (assumes one spawner; adjust if multiple)
            PointGiverSpawner spawner = FindObjectOfType<PointGiverSpawner>();
            if (spawner != null)
            {
                spawner.RespawnPetrol();
            }
            else
            {
                Debug.LogError("[GameManager] No PointGiverSpawner found for respawn!");
            }
        }
    }
}