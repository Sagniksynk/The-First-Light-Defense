using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    public event EventHandler OnResourceAmountChanged;
    private Dictionary<ResourceTypeSO, int> resourceDictionary;
    private ListOfResourceTypeSO listOfResourceType;
    void Awake()
    {
        Instance = this;
        resourceDictionary = new Dictionary<ResourceTypeSO, int>();
        listOfResourceType = Resources.Load<ListOfResourceTypeSO>(typeof(ListOfResourceTypeSO).Name);
        foreach (ResourceTypeSO resourceType in listOfResourceType.resourceTypes)
        {
            resourceDictionary[resourceType] = 0;
        }

    }
    // private void Test()
    // {
    //     foreach (ResourceTypeSO resourceType in resourceDictionary.Keys)
    //     {
    //         Debug.Log(resourceType.resourceName + ":" + resourceDictionary[resourceType]);
    //     }
    // }
    public void AddResources(ResourceTypeSO resourceType, int amount)
    {
        resourceDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);

    }
    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceDictionary[resourceType];
    }
    public bool CanAfford(List<ResourceCost> resourceCostList)
    {
        foreach (ResourceCost resourceCost in resourceCostList)
        {
            if (GetResourceAmount(resourceCost.resourceType) < resourceCost.amount)
            {
                return false;
            }
        }
        return true;
    }
    public void SpendResources(List<ResourceCost> resourceCostList)
    {
        foreach (ResourceCost resourceCost in resourceCostList)
        {
            resourceDictionary[resourceCost.resourceType] -= resourceCost.amount;
        }
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }
}
