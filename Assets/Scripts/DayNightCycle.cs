using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField]
    private Light2D globalLight; 

    [Header("Time Settings")]
    [Tooltip("The duration of a full day-night cycle in seconds.")]
    [SerializeField]
    private float dayDuration = 60f;

    [Range(0, 1)]
    private float timeOfDay;

    [Header("Color Gradients")]
    [Tooltip("Controls the color of the light throughout the day.")]
    [SerializeField]
    private Gradient lightColor;

    private void Update()
    {
        
        timeOfDay += Time.deltaTime / dayDuration;

        
        timeOfDay %= 1;

        
        UpdateLight();
    }

    private void UpdateLight()
    {
        if (globalLight != null)
        {
            
            globalLight.color = lightColor.Evaluate(timeOfDay);
        }
    }

    public void SetTimeOfDay(float newTime)
    {
        timeOfDay = Mathf.Clamp01(newTime);
        UpdateLight();
    }
}