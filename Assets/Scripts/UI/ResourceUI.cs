using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    private ListOfResourceTypeSO listOfResourceTypeSO;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;
    [SerializeField] private Transform resourceTemplate;
    [SerializeField] private float XOffset = -160f;
    void Awake()
    {
        listOfResourceTypeSO = Resources.Load<ListOfResourceTypeSO>(typeof(ListOfResourceTypeSO).Name);
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        resourceTemplate.gameObject.SetActive(false);
        int index = 0;
        foreach (ResourceTypeSO resourceType in listOfResourceTypeSO.resourceTypes)
        {
            Transform resourceTemplateGO = Instantiate(resourceTemplate, transform);
            resourceTemplateGO.gameObject.SetActive(true);
            resourceTemplateGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(XOffset * index, 0);
            resourceTemplateGO.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;
            resourceTypeTransformDictionary[resourceType] = resourceTemplateGO;
            index++;
        }
    }
    void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }
    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }
    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in listOfResourceTypeSO.resourceTypes)
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
