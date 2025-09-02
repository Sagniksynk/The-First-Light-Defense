using UnityEngine;
using TMPro;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private GameObject spriteGameObject;
    [SerializeField] private GameObject efficiencyGroup;
    [SerializeField] private TextMeshPro efficiencyText;
    [SerializeField] private SpriteRenderer iconRenderer;
    private SpriteRenderer ghostRenderer;
    private void Awake()
    {
        ghostRenderer = spriteGameObject.GetComponent<SpriteRenderer>();
        Hide();
    }
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
    }
    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e)
    {
        if (e.activeBuilidngType == null)
        {
            Hide();
        }
        else
        {
            Show(e.activeBuilidngType.sprite);
        }
    }
    private void Update()
    {
        if (!spriteGameObject.activeSelf) return;
        Vector3 mousePosition = UtilsClass.GetWorldPosition();
        transform.position = mousePosition;
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType != null)
        {
            bool canPlace = BuildingManager.Instance.CanPlaceBuliding(mousePosition, activeBuildingType);
            if (canPlace)
            {
                TooltipUI.Instance.Hide();
                SetGhostColor(102f, 181f, 214f, 231f);
            }
            else
            {
                SetGhostColor(215f, 121f, 121f, 231f);
            }

            if (activeBuildingType.hasResourceGeneratorData)
            {
                efficiencyGroup.SetActive(true);

                GeneratorData data = activeBuildingType.generatorData;
                int nearbyResourceAmount = BuildingManager.Instance.GetNearbyResourceAmount(mousePosition, data);
                iconRenderer.sprite = data.resourceType.sprite;

                if (data.maxResourceAmount > 0)
                {
                    float efficiency = (float)nearbyResourceAmount / data.maxResourceAmount;
                    efficiencyText.SetText(Mathf.RoundToInt(efficiency * 100f) + "%");
                }
                else
                {
                    efficiencyText.SetText("0%");
                }
            }
            else
            {
                efficiencyGroup.SetActive(false);
            }
        }
    }
    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        ghostRenderer.sprite = ghostSprite;
        SetGhostColor(102f, 181f, 214f, 231f);
    }
    private void Hide()
    {
        spriteGameObject.SetActive(false);
        if (efficiencyGroup != null)
        {
            efficiencyGroup.SetActive(false);
        }
    }
    private void SetGhostColor(float r, float g, float b, float a)
    {
        ghostRenderer.color = new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }
}