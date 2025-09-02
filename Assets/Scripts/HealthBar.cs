using UnityEngine;
using System;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("Color Settings")]
    [SerializeField] private Color highHealthColor = new Color(0.3f, 0.85f, 0.3f);
    [SerializeField] private Color lowHealthColor = new Color(0.85f, 0.2f, 0.2f);
    [SerializeField] [Range(0, 1)] private float lowHealthThreshold = 0.3f;

    private Transform barTransform;
    private SpriteRenderer barSpriteRenderer; // Changed from Image to SpriteRenderer
    private float targetScaleX;
    private float lerpSpeed = 8f;
    private Coroutine hideCoroutine;
    private float fullBarVisibleTime = 1.5f;

    private void Awake()
    {
        
        barTransform = transform.Find("bar");

        barSpriteRenderer = barTransform.GetComponentInChildren<SpriteRenderer>();

    }

    private void Start()
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealthGained += HealthSystem_OnHealthGained;

        targetScaleX = healthSystem.GetHealthAmountNormalized();
        UpdateBarInstant();
        UpdateBarColor();
        HealthBarVisibility();
    }

    private void Update()
    {
        float currentScaleX = barTransform.localScale.x;
        currentScaleX = Mathf.Lerp(currentScaleX, targetScaleX, Time.deltaTime * lerpSpeed);
        barTransform.localScale = new Vector3(currentScaleX, 1, 1);
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        targetScaleX = healthSystem.GetHealthAmountNormalized();
        UpdateBarColor();
        HealthBarVisibility();
    }

    private void HealthSystem_OnHealthGained(object sender, EventArgs e)
    {
        targetScaleX = healthSystem.GetHealthAmountNormalized();
        UpdateBarColor();

        if (healthSystem.IsHealthFull())
        {
            if (hideCoroutine != null) StopCoroutine(hideCoroutine);
            hideCoroutine = StartCoroutine(HideBarAfterDelay());
        }
        else
        {
            HealthBarVisibility();
        }
    }

    private void UpdateBarColor()
    {
        
        if (healthSystem.GetHealthAmountNormalized() <= lowHealthThreshold)
        {
            barSpriteRenderer.color = lowHealthColor;
        }
        else
        {
            barSpriteRenderer.color = highHealthColor;
        }
    }

    private IEnumerator HideBarAfterDelay()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(fullBarVisibleTime);
        HealthBarVisibility();
    }

    private void UpdateBarInstant()
    {
        barTransform.localScale = new Vector3(targetScaleX, 1, 1);
    }

    private void HealthBarVisibility()
    {
        if (healthSystem.IsHealthFull())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}