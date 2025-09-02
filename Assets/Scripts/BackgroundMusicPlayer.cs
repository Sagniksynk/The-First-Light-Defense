using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicPlayer : MonoBehaviour
{
    public static BackgroundMusicPlayer Instance { get; private set; }

    [Header("Music Tracks")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("Volume Settings")]
    [SerializeField] [Range(0f, 1f)] private float mainMenuFixedVolume = 0.5f;

    private AudioSource audioSource;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Persist across scenes
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        bool isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        audioSource.mute = isMuted;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // Manually call for the initial scene load
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Called every time a new scene is loaded to set the correct clip and volume.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip clipToPlay = null;
        float volumeToSet = mainMenuFixedVolume;

        // --- Determine which music and volume to use based on the scene ---
        if (scene.name == GameSceneManager.Scene.MainMenuScene.ToString())
        {
            clipToPlay = mainMenuMusic;
            // For the main menu, ALWAYS use the fixed default volume.
            volumeToSet = mainMenuFixedVolume;
        }
        else if (scene.name == GameSceneManager.Scene.GameScene.ToString())
        {
            clipToPlay = gameMusic;
            // For the game scene, use the volume set by the player's slider.
            volumeToSet = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        }

        // Apply the volume
        audioSource.volume = volumeToSet;

        // Switch the music only if the new track is different from the current one
        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }

    /// <summary>
    /// This method is now ONLY for the in-game options slider.
    /// </summary>
    public void SetVolume(float newVolume)
    {
        float clampedVolume = Mathf.Clamp01(newVolume);
        
        // Save the setting for the game scene
        PlayerPrefs.SetFloat("musicVolume", clampedVolume);
        PlayerPrefs.Save();

        // If we are currently in the game, update the volume immediately.
        if (SceneManager.GetActiveScene().name == GameSceneManager.Scene.GameScene.ToString())
        {
            audioSource.volume = clampedVolume;
        }
    }

    public void SetMute(bool mute)
    {
        audioSource.mute = mute;
    }

    // This method is no longer needed to set slider values, but can be kept for other uses.
    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("musicVolume", 0.5f);
    }
}