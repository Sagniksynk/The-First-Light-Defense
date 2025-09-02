using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public static event EventHandler OnGameOver;
    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;
    public class OnActiveBuildingTypeChangeEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuilidngType;
    }
    private Camera mainCamera;
    private BuildingTypeSO activeBuildingType;
    private ListOfBulidngTypeSO listOfBulidngType;
    private List<Vector3> placedBuildingPositions = new List<Vector3>();
    [SerializeField] private Transform HQPos;
    [SerializeField] private float maxConstructionRadius = 50f;
    void Awake()
    {
        Instance = this;
        listOfBulidngType = Resources.Load<ListOfBulidngTypeSO>(typeof(ListOfBulidngTypeSO).Name);
        activeBuildingType = null;
    }
    private void Start()
    {
        mainCamera = Camera.main;
        HQPos.GetComponent<HealthSystem>().OnDied += HQ_OnDied;
    }

    private void HQ_OnDied(object sender, EventArgs e)
    {
        GameOverUI.Instance.Show();
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            TryPlaceBuilding();
        }
        //CycleBuildingType();
        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 spawnPos = UtilsClass.GetWorldPosition() + UtilsClass.GetRandomDirection() * 5f;
            Enemy.CreateEnemy(spawnPos);
        }

    }

    private void TryPlaceBuilding()
    {
        TooltipUI.Instance.Hide();
        if (activeBuildingType == null) return;
        Vector3 placePosition = UtilsClass.GetWorldPosition();
        if (!ResourceManager.Instance.CanAfford(activeBuildingType.constructionCostList))
        {
            TooltipUI.Instance.Show("<color=#DE3838>Not enough resources!</color>");
        }
        if (!CanPlaceBuliding(placePosition, activeBuildingType))
        {
            return;
        }
        ResourceManager.Instance.SpendResources(activeBuildingType.constructionCostList);
        //GameObject instantiatedBuilding = Instantiate(activeBuildingType.buildingPrefab, UtilsClass.GetWorldPosition(), Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
        BuildingConstruction.Create(UtilsClass.GetWorldPosition(), activeBuildingType);
        placedBuildingPositions.Add(placePosition);
        TooltipUI.Instance.Hide();
    }
    // private void CycleBuildingType()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         activeBuildingType = listOfBulidngType.buildingTypes[0];
    //     }
    //     if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         activeBuildingType = listOfBulidngType.buildingTypes[1];
    //     }
    // }

    public bool CanPlaceBuliding(Vector3 position, BuildingTypeSO buildingType)
    {
        if (!ResourceManager.Instance.CanAfford(buildingType.constructionCostList))
        {
            return false;
        }
        BoxCollider2D boxCollider2D = buildingType.buildingPrefab.GetComponent<BoxCollider2D>();
        if (boxCollider2D == null)
        {
            Debug.LogWarning("No Collider2D found!");
            return true;
        }
        Vector2 offSet = boxCollider2D.offset;
        Vector2 size = boxCollider2D.size;
        Vector2 worldPos = (Vector2)position + offSet;
        Collider2D[] hits = Physics2D.OverlapBoxAll(worldPos, size, 0f);
        bool isAreaClear = hits.Length == 0;
        if (!isAreaClear)
        {
            TooltipUI.Instance.Show("<color=#DE3838>Area not clear! </color>");
            return false;
        } 
        hits = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D hit in hits)
        {
            BuildingTypeHolder buildingTypeHolder = hit.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    TooltipUI.Instance.Show("<color=#DE3838>Too close to a " + buildingType.buildingName + "</color>");
                    return false;
                }
            }
        }

        hits = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D hit in hits)
        {
            BuildingTypeHolder buildingTypeHolder = hit.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                return true;
            }
        }
        TooltipUI.Instance.Show("<color=#DE3838>Too far from HQ! </color>");
        return false;
    }
    public int GetNearbyResourceAmount(Vector3 position, GeneratorData generatorData)
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position, generatorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == generatorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, generatorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChange?.Invoke(this, new OnActiveBuildingTypeChangeEventArgs { activeBuilidngType = activeBuildingType });
    }
    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }
    public Transform GetHQBuildingTransform()
    {
        return HQPos;
    }
    private void OnDrawGizmos()
    {
        if (activeBuildingType == null) return;
        Gizmos.color = Color.red;
        foreach (Vector3 pos in placedBuildingPositions)
        {
            Gizmos.DrawWireSphere(pos, activeBuildingType.minConstructionRadius);

        }
        if(HQPos!=null)
        Gizmos.DrawWireSphere(HQPos.position, maxConstructionRadius);
    }
}
