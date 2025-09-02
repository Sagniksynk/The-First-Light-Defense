using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }
    private EnemyWaveManager enemyWaveManager;
    private void Awake()
    {
        Instance = this;
        enemyWaveManager = FindFirstObjectByType<EnemyWaveManager>();
        transform.Find("retryBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        transform.Find("waveSurviveText").GetComponent<TextMeshProUGUI>().SetText("You survived "+ enemyWaveManager.GetWaveCount()+" waves !");
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
