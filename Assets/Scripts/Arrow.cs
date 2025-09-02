using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Transform targetTransform;
    private float moveSpeed;
    private float maxMoveSpeed;
    private Vector3 trajectoryRange;
    private float trajectoryMaxRelativeHeight;
    private AnimationCurve trajectoryAnimationCurve;
    private AnimationCurve axisCorrectionAnimationCurve;
    private AnimationCurve projectileSpeedAnimationCurve;
    private Vector3 trajectoryStartPoint;

    private float nextXTrajectoryPosition;
    private float nextYTrajectoryPosition;
    private float nextPositionXCorrectionAbosolute;
    private float nextPositionYCorrectionAbosolute;

    private void Start()
    {
        trajectoryStartPoint = transform.position;
    }
    private void Update()
    {
        if (targetTransform == null)
        {
            Destroy(gameObject); // Destroy the arrow if the target no longer exists
            return;
        }

        // Move the arrow towards the target
        UpdateProjectilePosition();

        // Destroy the arrow if it gets close enough to the target
        if (Vector3.Distance(transform.position, targetTransform.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
    private void UpdateProjectilePosition()
    {
        trajectoryRange = targetTransform.position - trajectoryStartPoint;
        if (Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
        {
            if (trajectoryRange.y < 0)
            {
                moveSpeed = -moveSpeed;
            }
            UpdatePositionWithXCurve();
        }
        else
        {
            if (trajectoryRange.x < 0)
            {
                moveSpeed = -moveSpeed;
            }
            UpdatePositionWithYCurve();
        }
        

    }

    private void UpdatePositionWithXCurve()
    {
        float nextPositionY = transform.position.y + moveSpeed * Time.deltaTime;
        float nextPositionYNormalized = (nextPositionY - trajectoryStartPoint.y) / trajectoryRange.y;

        float nextPositionXNormalized = trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);
        float nextPositionXCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionYNormalized);
        nextXTrajectoryPosition = nextPositionXNormalized * trajectoryMaxRelativeHeight;
        nextPositionXCorrectionAbosolute = nextPositionXCorrectionNormalized * trajectoryRange.x;

        if (trajectoryRange.x > 0 && trajectoryRange.y > 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }
        if (trajectoryRange.x < 0 && trajectoryRange.y < 0)
        {
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        float nextPositionX = trajectoryStartPoint.x + nextXTrajectoryPosition + nextPositionXCorrectionAbosolute;

        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);
        CalculateNextProjectileSpeed(nextPositionYNormalized);
        Vector3 moveDirection = (newPosition - transform.position).normalized;
        transform.right = moveDirection;
        transform.position = newPosition;
    }

    private void UpdatePositionWithYCurve()
    {
        float nextPositionX = transform.position.x + moveSpeed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;

        float nextPositionYNormalized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        float nextPositionYCorrectionNormalized = axisCorrectionAnimationCurve.Evaluate(nextPositionXNormalized);
        nextYTrajectoryPosition = nextPositionYNormalized * trajectoryMaxRelativeHeight;
        nextPositionYCorrectionAbosolute = nextPositionYCorrectionNormalized * trajectoryRange.y;
        float nextPositionY = trajectoryStartPoint.y + nextYTrajectoryPosition + nextPositionYCorrectionAbosolute;

        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);
        CalculateNextProjectileSpeed(nextPositionXNormalized);
        Vector3 moveDirection = (newPosition - transform.position).normalized;
        transform.right = moveDirection;
        transform.position = newPosition;
    }

    private void CalculateNextProjectileSpeed(float nextPositionXNormalized)
    {
        float nextMoveSpeedNormalized = projectileSpeedAnimationCurve.Evaluate(nextPositionXNormalized);
        moveSpeed = nextMoveSpeedNormalized * maxMoveSpeed;
    }
    public void InitializeProjectile(Transform target, float maxMoveSpeed, float trajectoryMaxHeight)
    {
        this.targetTransform = target;
        this.maxMoveSpeed = maxMoveSpeed;

        float xDistanceToTarget = targetTransform.position.x - transform.position.x;
        this.trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget) * trajectoryMaxHeight;
    }
    public void InitializeAnimationCurve(AnimationCurve animationCurve, AnimationCurve axisCorrectionAnimationCurve, AnimationCurve projectileSpeedAnimationCurve)
    {
        this.trajectoryAnimationCurve = animationCurve;
        this.axisCorrectionAnimationCurve = axisCorrectionAnimationCurve;
        this.projectileSpeedAnimationCurve = projectileSpeedAnimationCurve;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && collision.transform == targetTransform)
        {
            SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
            enemy.Die(); // Destroy the enemy and play particle effect
            Destroy(gameObject); // Destroy the arrow
        }
    }
}