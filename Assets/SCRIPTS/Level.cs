using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("highwayRace");
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
