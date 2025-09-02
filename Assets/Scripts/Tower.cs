using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Attributes")]
    [SerializeField] private float detectionRadius;
    [SerializeField] private float fireInterval = 1.5f; // Time in seconds between each shot
    [SerializeField] private float detectionInterval = 0.2f; // How often to check for new targets

    [Header("Projectile Settings")]
    [SerializeField] private float arrowMoveSpeed;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private AnimationCurve axisCorrectionAnimationCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimationCurve;
    [SerializeField] private float projectileMaxHeight;
    
    private Transform targetTransform;
    private GameObject arrowPrefab;
    private float fireCooldownTimer;
    private float detectionTimer;

    private bool isGameOver = false;

    private void Start()
    {
        arrowPrefab = Resources.Load<GameObject>("Arrow");
        fireCooldownTimer = fireInterval;
        detectionTimer = 0f;

        BuildingManager.OnGameOver += BuildingManager_OnGameOver;
    }

    private void OnDestroy()
    {
        BuildingManager.OnGameOver -= BuildingManager_OnGameOver;
    }

    private void Update()
    {
        if (isGameOver) return;
        detectionTimer -= Time.deltaTime;
        if (detectionTimer <= 0f)
        {
            detectionTimer = detectionInterval;
            FindClosestEnemy();
        }

        fireCooldownTimer -= Time.deltaTime;

        if (targetTransform != null && fireCooldownTimer <= 0f)
        {
            fireCooldownTimer = fireInterval;
            SoundManager.Instance.PlaySound(SoundManager.Sound.TowerShoot);
            Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.InitializeProjectile(targetTransform, arrowMoveSpeed, projectileMaxHeight);
            arrow.InitializeAnimationCurve(trajectoryAnimationCurve, axisCorrectionAnimationCurve, projectileSpeedAnimationCurve);
        }
    }
        private void BuildingManager_OnGameOver(object sender, EventArgs e)
        {
        isGameOver = true;
        }

    private void FindClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }
        }

        targetTransform = closestEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}