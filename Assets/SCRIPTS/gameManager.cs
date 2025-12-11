using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Level levelScript; // assign in inspector
    public ScoreManager scoreManager; // assign in inspector (optional now)
    public int requiredScore = 10;
    private bool endHandled = false;

    // Survival win for Level 2
    private float survivalTime = 17f; 
    private Coroutine survivalTimerCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        // Re-find levelScript
        levelScript = FindObjectOfType<Level>();
        Debug.Log($"[GameManager] Scene loaded: {scene.name}. levelScript found: {levelScript != null}");

        endHandled = false;

        // FIXED: Stop any existing timer first to prevent carryover
        if (survivalTimerCoroutine != null)
        {
            StopCoroutine(survivalTimerCoroutine);
            survivalTimerCoroutine = null;
            Debug.Log("[GameManager] Existing timer stopped on scene load.");
        }

        // Start timer only in Level 2 (use your scene name from console)
        if (scene.name == "highwayRace2")
        {
            Debug.Log("[GameManager] Level 2 (highwayRace2) loaded! Starting 10s survival timer...");
            survivalTimerCoroutine = StartCoroutine(SurvivalTimer());
        }
        else
        {
            Debug.Log("[GameManager] Scene is " + scene.name + " – NOT starting timer (Level 1 or other).");
        }
    }

    // Timer coroutine
    private IEnumerator SurvivalTimer()
    {
        yield return new WaitForSeconds(survivalTime);

        if (!endHandled)
        {
            Debug.Log("[GameManager] 10s survived in Level 2! Loading winScene...");
            if (levelScript != null)
            {
                levelScript.LoadWinScene();
            }
            else
            {
                SceneManager.LoadScene("winScene"); 
            }
        }
    }

    public void WinLevel()
    {
        if (endHandled) return;
        endHandled = true;
        Debug.Log("[GameManager] Collected enough petrol! -> Load Level 2");
        levelScript.LoadGameScene2();
    }

    public void PlayerDied()
    {
        if (endHandled) return;
        endHandled = true;
        Debug.Log("[GameManager] Player died -> Load Game Over scene");
        levelScript.LoadGameOverScene();
    }

    public void AllPetrolFinished()
    {
        if (endHandled) return;
        endHandled = true;
        int currentScore = ScoreManager.Instance != null ? ScoreManager.Instance.score : 0;
        if (currentScore >= requiredScore)
        {
            levelScript.LoadGameScene2();
        }
        else
        {
            levelScript.LoadGameOverScene();
        }
    }
}