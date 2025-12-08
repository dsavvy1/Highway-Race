using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Assignments")]
    public Level levelScript; // Auto-assigned below; Inspector for override
    public ScoreManager scoreManager; // assign in inspector (optional now)
    public int requiredScore = 10;
    private bool endHandled = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Auto-find Level script in current scene
        if (levelScript == null)
        {
            levelScript = FindObjectOfType<Level>();
            if (levelScript == null)
                Debug.LogError("[GameManager] No Level script found in scene!");
        }

        // Reset for new level
        endHandled = false;
        ResetLevelState();
    }

    // Listen for scene loads to re-find Level & reset
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[GameManager] Scene '{scene.name}' loaded.");

        // Re-find Level for new scene
        if (levelScript == null)
        {
            levelScript = FindObjectOfType<Level>();
            Debug.Log($"[GameManager] Assigned levelScript: {levelScript != null}");
        }

        // FIXED: Reset score for NEW LEVEL (prevents carryover triggering instant win)
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        endHandled = false;
        ResetLevelState();
    }

    private void ResetLevelState()
    {
        // Additional resets if needed (e.g., spawner activePetrol via FindObjectOfType)
        Debug.Log("[GameManager] Level state reset.");
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
            Debug.LogError("[GameManager] levelScript missing for WinLevel!");
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
            Debug.LogError("[GameManager] levelScript missing for PlayerDied!");
    }

    public void AllPetrolFinished()
    {
        if (endHandled) return;

        int currentScore = ScoreManager.Instance != null ? ScoreManager.Instance.score : 0;
        Debug.Log($"[GameManager] All petrol finished. score={currentScore}, required={requiredScore}");

        if (currentScore >= requiredScore)
        {
            WinLevel();
        }
        else
        {
            Debug.Log("[GameManager] Not enough score, but respawning petrol for another try...");
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