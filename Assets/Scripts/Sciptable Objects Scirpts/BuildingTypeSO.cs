using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceCost
{
    public ResourceTypeSO resourceType;
    public int amount;
}

[CreateAssetMenu(menuName = "Scriptable Objects/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    public string buildingName;
    public GameObject buildingPrefab;
    public GeneratorData generatorData;
    public bool hasResourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public List<ResourceCost> constructionCostList;
    public int healthAmountMax;
    public float constructionTimerMax;
    public string GetConstructionCostResourceString()
    {
        string str = "";
        foreach (ResourceCost resourceCost in constructionCostList)
        {
            str += resourceCost.resourceType.resourceName + ": <color=#15EC00>" + resourceCost.amount + "</color> ";
        }
        return str;
    }
}
