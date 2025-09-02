using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    private ResourceGenerator resourceGenerator;
    private Transform barTransform;
    private Animator buildingAnimator;

    private void Awake()
    {
        resourceGenerator = GetComponentInParent<ResourceGenerator>();

        if (resourceGenerator == null)
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        GeneratorData generatorData = resourceGenerator.GetResourceGeneratorData();
        barTransform = transform.Find("bar");
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = generatorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGenerated().ToString("F1"));
        if (transform.parent != null)
        {
            buildingAnimator = transform.parent.GetComponentInChildren<Animator>();
        }
    }
    private void Update()
    {
        float normalized = resourceGenerator.enabled ? resourceGenerator.GetTimerNormalized() : 0f;
        barTransform.localScale = resourceGenerator.enabled
        ? new Vector3(1 - normalized, 1f, 1f)
        : new Vector3(0f, 1f, 1f);
        if (buildingAnimator != null)
        {
            buildingAnimator.enabled = resourceGenerator.enabled;
        }
    }
}