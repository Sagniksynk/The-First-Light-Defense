using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpritePositionSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool willRunOnce;
    [SerializeField] private float YPositionOffset;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y+YPositionOffset) * -100);
        if (willRunOnce) Destroy(this);
    }
}
