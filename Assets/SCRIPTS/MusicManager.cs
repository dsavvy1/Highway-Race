using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip introMusic;
    public AudioClip backgroundMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string name = scene.name;

        if (name == "startMenuScene" || name == "instructionScene")
            Play(introMusic);

        else if (name == "highwayRace" || name == "highwayRace2")
            Play(backgroundMusic);

        else if (name == "gameOverScene")
            Play(loseMusic);

        else if (name == "winScene")
            Play(winMusic);
    }

    void Play(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // Don't restart same music
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
