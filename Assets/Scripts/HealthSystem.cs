using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private BuildingTypeSO buildingTypeSO;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealthGained;
    public event EventHandler OnDied;
    private int healthAmountMax;
    private int healthAmount;
    void Awake()
    {
        healthAmount = healthAmountMax;
    }
    public void Damage(int damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead()) OnDied?.Invoke(this, EventArgs.Empty);
    }
    public void AddHeathTester(int heath)
    {
        healthAmount += heath;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnHealthGained?.Invoke(this, EventArgs.Empty);
    }
    public bool IsDead()
    {
        return healthAmount == 0;
    }
    public int GetHealthAmount()
    {
        return healthAmount;
    }
    public bool IsHealthFull()
    {
        return healthAmount == healthAmountMax;
    }
    public float GetHealthAmountNormalized()
    {
        
        return (float)healthAmount / healthAmountMax;
    }
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax;
        }
    }
}
