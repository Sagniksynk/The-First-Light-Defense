using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Enum to define all the sound effects in the game for easy reference.
    public enum Sound
    {
        BuildingPlaced,
        BuildingConstructed,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
        TowerShoot
    }

    // Singleton instance to ensure only one SoundManager exists.
    public static SoundManager Instance { get; private set; }

    [Header("Audio Configuration")]
    [SerializeField] private SoundAudioClip[] soundAudioClipArray;
    private AudioSource audioSource;
    private float volume = 1f;

    // A serializable class to link Sound enums to AudioClip files in the Inspector.
    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
        [Range(0f, 2f)]
        public float volume = 1f; 
    }

    private void Awake()
    {
        // Implement the singleton pattern.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Get the AudioSource component attached to this GameObject.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource if one doesn't exist.
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Load the saved volume from PlayerPrefs, defaulting to 1f.
        volume = PlayerPrefs.GetFloat("soundVolume", 1f);
        audioSource.volume = volume;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Plays a specified sound effect once.
    /// </summary>
    /// <param name="sound">The sound effect to play from the Sound enum.</param>
    public void PlaySound(Sound sound)
    {
        SoundAudioClip soundAudioClip = GetSoundAudioClip(sound); // Get the whole object, not just the clip
        if (soundAudioClip != null)
        {
            // Play the clip using its own volume multiplier
            audioSource.PlayOneShot(soundAudioClip.audioClip, this.volume * soundAudioClip.volume);
        }
        else
        {
            Debug.LogWarning("Sound " + sound + " not found!");
        }
    }

    /// <summary>
    /// Changes the volume of the sound effects.
    /// </summary>
    /// <param name="newVolume">The new volume level (clamped between 0 and 1).</param>
    public void ChangeVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("soundVolume", volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Retrieves the AudioClip associated with a specific sound.
    /// </summary>
    /// <param name="sound">The sound enum to find the clip for.</param>
    /// <returns>The corresponding AudioClip, or null if not found.</returns>
    private SoundAudioClip GetSoundAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip;
            }
        }
        return null;
    }

    public float GetVolume()
    {
        return volume;
    }
}