using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(AudioSource))]
public class BuildingSoundPlayer : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip ambientSoundClip;
    [SerializeField] private float maxSoundDistance = 30f; // Zoom level where sound is fully silent
    [SerializeField] private float minSoundDistance = 15f; // Zoom level where sound is at full volume

    private AudioSource audioSource;
    private CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Find the main Cinemachine camera in the scene
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();

        if (cinemachineCamera == null)
        {
            Debug.LogError("CinemachineCamera not found in the scene! Building sounds won't work.");
            enabled = false; // Disable the script if no camera is found
            return;
        }

        // Configure the AudioSource for looping ambient sound
        audioSource.clip = ambientSoundClip;
        audioSource.loop = true;
        audioSource.spatialBlend = 0f; // Make it a 2D sound so volume is controlled by script, not distance
        audioSource.volume = 0f; // Start silent
        audioSource.Play();
    }

    void Update()
    {
        // Get the camera's current zoom level (orthographic size)
        float cameraZoom = cinemachineCamera.Lens.OrthographicSize;

        // Calculate how loud the sound should be based on the zoom
        // InverseLerp returns 0 if zoom is at maxDistance, 1 if at minDistance
        float targetVolume = Mathf.InverseLerp(maxSoundDistance, minSoundDistance, cameraZoom);

        // Smoothly adjust the volume to the target volume
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * 5f);
    }
}