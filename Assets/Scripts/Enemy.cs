using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy CreateEnemy(Vector3 position)
    {
        Transform enemyPrefab = Resources.Load<Transform>("Enemy");
        Transform enemyTransform = Instantiate(enemyPrefab, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private Transform targetBuildingTransform;
    private Rigidbody2D rb2D;
    [SerializeField] private float detectionRadius = 10f; // Detection radius for buildings
    [SerializeField] private float speed = 6f;
    [SerializeField] private GameObject deathParticlePrefab;
    private float detectionInterval = 0.5f; 

    private float detectionTimer;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        FindClosestBuildingOrHQ();
        detectionTimer = Random.Range(0f,detectionInterval);
    }

    private void Update()
    {
        // Continuously detect the closest building at regular intervals
        detectionTimer -= Time.deltaTime;
        if (detectionTimer <= 0f)
        {
            FindClosestBuildingOrHQ();
            detectionTimer = detectionInterval;
        }

        // Move towards the target building
        if (targetBuildingTransform != null)
        {
            Vector2 direction = (targetBuildingTransform.position - transform.position).normalized;
            rb2D.linearVelocity = direction * speed;
        }
        else
        {
            rb2D.linearVelocity = Vector2.zero;
        }
    }

    private void FindClosestBuildingOrHQ()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        float closestDistance = Mathf.Infinity;
        Transform closestBuilding = null;

        foreach (Collider2D hit in hits)
        {
            Building building = hit.GetComponent<Building>();
            if (building != null)
            {
                float distance = Vector2.Distance(transform.position, building.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBuilding = building.transform;
                }
            }
        }

        // If no building is found in the radius, default to HQ
        if (closestBuilding != null)
        {
            targetBuildingTransform = closestBuilding;
        }
        else
        {
            targetBuildingTransform = BuildingManager.Instance.GetHQBuildingTransform();
        }
    }
    public void Die()
    {
        if (deathParticlePrefab != null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        ScreenShake.Instance.Shake(0.3f);
        PostProcessing_ChromaticAberration.Instance.SetWeight(0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(20);
            Die();
        }
    }

}