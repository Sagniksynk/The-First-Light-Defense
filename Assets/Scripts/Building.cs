using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem healthSystem;
    private BuildingTypeSO buildingTypeSO;
    private void Awake()
    {
        buildingTypeSO = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingTypeSO.healthAmountMax, true);
    }
    private void Start()
    {
        healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
        ScreenShake.Instance.Shake(1f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        PostProcessing_ChromaticAberration.Instance.SetWeight(1f);
    }
}
