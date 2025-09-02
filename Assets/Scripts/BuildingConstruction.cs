using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType)
    {
        Transform buildingConstructionPrefab = Resources.Load<Transform>("BuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(buildingConstructionPrefab, position, Quaternion.identity);
        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);
        return buildingConstruction;
    }
    private BuildingTypeSO buildingType;
    private float constructionTimer;
    private float constructionTimerMax;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject buildingConstructionParticle;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        Instantiate(buildingConstructionParticle, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        if (constructionTimer <= 0f)
        {
            Instantiate(buildingType.buildingPrefab, transform.position, Quaternion.identity);
            Instantiate(buildingConstructionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;
        constructionTimerMax = buildingType.constructionTimerMax;
        constructionTimer = constructionTimerMax;
        spriteRenderer.sprite = buildingType.sprite;
        boxCollider2D.offset = buildingType.buildingPrefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.buildingPrefab.GetComponent<BoxCollider2D>().size;
    }
    public float GetConstructionTimerNormalized()
    {
        return 1 - constructionTimer / constructionTimerMax;
    }
}
