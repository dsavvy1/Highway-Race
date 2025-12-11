using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Level levelScript;
    public ScoreManager scoreManager;
    public int requiredScore = 10;
    private bool endHandled = false;

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
        levelScript = FindObjectOfType<Level>();
        Debug.Log($"[GameManager] Scene loaded: {scene.name}. levelScript found: {levelScript != null}");

        endHandled = false;

        if (survivalTimerCoroutine != null)
        {
            StopCoroutine(survivalTimerCoroutine);
            survivalTimerCoroutine = null;
            Debug.Log("[GameManager] Existing timer stopped on scene load.");
        }

        if (scene.name == "highwayRace2")
        {
            Debug.Log("[GameManager] Level 2 loaded -> starting survival timer...");
            survivalTimerCoroutine = StartCoroutine(SurvivalTimer());
        }
    }

    private IEnumerator SurvivalTimer()
    {
        yield return new WaitForSeconds(survivalTime);

        if (!endHandled)
        {
            Debug.Log("[GameManager] 17 seconds survived -> WIN");
            if (levelScript != null)
                levelScript.LoadWinScene();
            else
                SceneManager.LoadScene("winScene");
        }
    }

    public void WinLevel()
    {
        if (endHandled) return;
        endHandled = true;

        Debug.Log("[GameManager] Required petrol collected -> Load Level 2");
        levelScript.LoadGameScene2();
    }

    public void PlayerDied()
    {
        if (endHandled) return;
        endHandled = true;

        Debug.Log("[GameManager] Player died -> Game Over");
        levelScript.LoadGameOverScene();
    }
}
