using UnityEngine;
using UnityEngine.UI;

public class SpawnIndicatorUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    [SerializeField] private float borderSize = 50f;

    private RectTransform arrowRectTransform;
    private Image arrowImage;
    private Camera mainCamera;
    private bool isWaitingForWave = false; // Track if we're in waiting state

    private void Awake()
    {
        arrowRectTransform = GetComponentInChildren<RectTransform>();
        arrowImage = GetComponentInChildren<Image>();
        mainCamera = Camera.main;
    }

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
        arrowImage.enabled = false;
    }
    
    private void HandleWaveStateChanged(EnemyWaveManager.State state)
    {
        // Track the wave state but don't directly control visibility here
        isWaitingForWave = (state == EnemyWaveManager.State.WaitingForNextWave);
    }

    private void Update()
    {
        // Don't process anything if we're not waiting for a wave
        if (!isWaitingForWave)
        {
            arrowImage.enabled = false;
            return;
        }

        Vector3 targetWorldPosition = enemyWaveManager.GetCurrentWaveSpawnTransform();
        Vector3 targetScreenPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);

        // Check if target is off screen (outside the visible camera bounds)
        bool isOffScreen = targetScreenPosition.z <= 0 || // Behind camera
                           targetScreenPosition.x < 0 ||
                           targetScreenPosition.x > Screen.width ||
                           targetScreenPosition.y < 0 ||
                           targetScreenPosition.y > Screen.height;

        // Only show arrow when target is off screen AND we're waiting for wave
        arrowImage.enabled = isOffScreen;

        // Only update position and rotation if arrow is visible
        if (arrowImage.enabled)
        {
            Vector3 clampedPosition = targetScreenPosition;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, borderSize, Screen.width - borderSize);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, borderSize, Screen.height - borderSize);
            arrowRectTransform.position = clampedPosition;

            Vector3 direction = (targetScreenPosition - arrowRectTransform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowRectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}