using UnityEngine;
using Unity.Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    /// <summary>
    /// Triggers a screen shake.
    /// </summary>
    /// <param name="intensity">The force of the shake.</param>
    public void Shake(float intensity = 1f)
    {
        impulseSource.GenerateImpulse(intensity);
    }
}