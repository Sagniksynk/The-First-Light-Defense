using UnityEngine;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField]private EnemyWaveManager enemyWaveManager;
    [SerializeField] private float blinkspeed = 5f;
    private TextMeshProUGUI waveCountText;
    private TextMeshProUGUI nextWaveTimer;

    private void OnEnable()
    {
        EnemyWaveManager.OnStateChanged += HandleWaveStateChanged;
    }
    private void OnDisable()
    {
        EnemyWaveManager.OnStateChanged -= HandleWaveStateChanged;
    }
    private void Start()
    {
        waveCountText = transform.Find("wavecounter").GetComponent<TextMeshProUGUI>();
        nextWaveTimer = transform.Find("nextwavetimer").GetComponent<TextMeshProUGUI>();
    }
    private void HandleWaveStateChanged(EnemyWaveManager.State state)
    {
        if (state == EnemyWaveManager.State.SpawningWave)
        {
            nextWaveTimer.gameObject.SetActive(false);
        }
        else
        {
            nextWaveTimer.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        waveCountText.SetText("Current Wave: " + enemyWaveManager.GetWaveCount());
        if (nextWaveTimer.gameObject.activeSelf)
        {
            float timer = enemyWaveManager.GetNextWaveTimer();
            nextWaveTimer.SetText("Next Wave in: " + timer.ToString("F1") + " 's");

            if (timer < 4f)
            {
                float alpha = (Mathf.Sin(Time.time * blinkspeed) + 1f) / 2f;
                nextWaveTimer.alpha = alpha;
            }
            else
            {
                nextWaveTimer.alpha = 1f;
            }
            
        }
        
    }

}