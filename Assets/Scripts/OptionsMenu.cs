using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Slider soundVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    private void Awake()
    {
        Instance = this;
        optionsPanel.SetActive(false); // Start with the menu hidden
    }

    private void Start()
    {
        // --- Button Listeners ---
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f; // Ensure time is resumed before leaving scene
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        // --- Slider Listeners ---
        soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        // Initialize sliders with current volume settings
        InitializeSliders();
    }

    /// <summary>
    /// Toggles the options menu on and off.
    /// </summary>
    public void ToggleOptionsMenu()
    {
        if (optionsPanel.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Freeze the game
        optionsPanel.SetActive(true);
        InitializeSliders(); // Refresh slider values when opening
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Unfreeze the game
        optionsPanel.SetActive(false);
    }

    private void SetSoundVolume(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ChangeVolume(value);
        }
    }

    private void SetMusicVolume(float value)
    {
        if (BackgroundMusicPlayer.Instance != null)
        {
            BackgroundMusicPlayer.Instance.SetVolume(value);
        }
    }

    /// <summary>
    /// Sets the sliders to reflect the current audio volumes.
    /// </summary>
    private void InitializeSliders()
    {
        if (SoundManager.Instance != null)
        {
            soundVolumeSlider.value = SoundManager.Instance.GetVolume();
        }
        if (BackgroundMusicPlayer.Instance != null)
        {
            musicVolumeSlider.value = BackgroundMusicPlayer.Instance.GetVolume();
        }
    }
}