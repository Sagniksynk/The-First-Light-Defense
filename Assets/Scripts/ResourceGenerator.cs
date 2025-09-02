// ResourceGenerator.cs
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    private GeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;
    void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.generatorData;
        timerMax = resourceGeneratorData.timerMax;
    }
    private void Start()
    {
        // The resource detection logic is now replaced by a single call
        int nearbyResourceAmount = BuildingManager.Instance.GetNearbyResourceAmount(transform.position, resourceGeneratorData);
        
        if (nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }
        //Debug.Log("NearbyRamm: " + nearbyResourceAmount + ", " + timerMax);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timerMax;
            ResourceManager.Instance.AddResources(resourceGeneratorData.resourceType, 1);
        }
    }
    public GeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }
    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }
    public float GetAmountGenerated()
    {
        if (!enabled) return 0f;
        return 1 / timerMax;
    }

}