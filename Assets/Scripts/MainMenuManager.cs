using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button muteButton;
    [SerializeField] private Button backFromHowToPlayButton;

    [Header("Panels")]
    [SerializeField] private GameObject mainButtonsPanel;
    [SerializeField] private GameObject howToPlayPanel;

    [Header("Mute Sprites")]
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;

    private bool isMuted = false;

    private void Start()
    {
        // --- Add Button Listeners ---
        newGameButton.onClick.AddListener(StartNewGame);
        howToPlayButton.onClick.AddListener(ShowHowToPlay);
        quitButton.onClick.AddListener(QuitGame);
        muteButton.onClick.AddListener(ToggleMusic);
        backFromHowToPlayButton.onClick.AddListener(ShowMainMenu);

        // --- Initial State ---
        mainButtonsPanel.SetActive(true);
        howToPlayPanel.SetActive(false);

        // --- Load Mute Preference ---
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;
        UpdateMuteButtonIcon();
        if (BackgroundMusicPlayer.Instance != null)
        {
            BackgroundMusicPlayer.Instance.SetMute(isMuted);
        }
    }

    private void StartNewGame()
    {
        GameSceneManager.Load(GameSceneManager.Scene.GameScene);
    }

    private void ShowHowToPlay()
    {
        mainButtonsPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    private void ShowMainMenu()
    {
        howToPlayPanel.SetActive(false);
        mainButtonsPanel.SetActive(true);
    }

    private void QuitGame()
    {
        // This works in a built game.
        Application.Quit();

        // This is to stop the editor when testing.
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void ToggleMusic()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        if (BackgroundMusicPlayer.Instance != null)
        {
            BackgroundMusicPlayer.Instance.SetMute(isMuted);
        }

        UpdateMuteButtonIcon();
    }

    private void UpdateMuteButtonIcon()
    {
        if (muteButton != null && musicOnSprite != null && musicOffSprite != null)
        {
            muteButton.GetComponent<Image>().sprite = isMuted ? musicOffSprite : musicOnSprite;
        }
    }
}
