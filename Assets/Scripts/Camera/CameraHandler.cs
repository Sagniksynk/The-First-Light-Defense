using UnityEngine;
using Unity.Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float cameraMoveSpeed = 20f;
    [SerializeField] private float edgeScrollSize = 30f; // The size of the border in pixels for edge scrolling

    [Header("Zoom Settings")]
    [SerializeField] private float zoomingSpeed = 10f;
    [SerializeField] private float zoomStep = 2f;
    [SerializeField] private float minOrthographicSize = 7f;
    [SerializeField] private float maxOrthographicSize = 35f;

    [Header("References")]
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float currentOrthoSize;
    private float targetOrthoSize;

    private void Start()
    {
        currentOrthoSize = cinemachineCamera.Lens.OrthographicSize;
        targetOrthoSize = currentOrthoSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    /// <summary>
    /// Handles camera panning with WASD / Arrow Keys and mouse edge scrolling.
    /// </summary>
    private void HandleMovement()
    {
        Vector3 inputDir = Vector3.zero;

        // Keyboard input
        inputDir.x = Input.GetAxisRaw("Horizontal");
        inputDir.y = Input.GetAxisRaw("Vertical");

        // Mouse edge scrolling input
        if (Input.mousePosition.x < edgeScrollSize)
        {
            inputDir.x = -1f;
        }
        if (Input.mousePosition.x > Screen.width - edgeScrollSize)
        {
            inputDir.x = 1f;
        }
        if (Input.mousePosition.y < edgeScrollSize)
        {
            inputDir.y = -1f;
        }
        if (Input.mousePosition.y > Screen.height - edgeScrollSize)
        {
            inputDir.y = 1f;
        }

        // Apply movement
        Vector3 moveDir = inputDir.normalized;
        transform.position += moveDir * cameraMoveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Handles zooming in/out with mouse scroll wheel.
    /// </summary>
    private void HandleZoom()
    {
        // Adjust target zoom based on scroll wheel
        targetOrthoSize += -Input.mouseScrollDelta.y * zoomStep;
        targetOrthoSize = Mathf.Clamp(targetOrthoSize, minOrthographicSize, maxOrthographicSize);

        // Smooth zooming
        currentOrthoSize = Mathf.Lerp(currentOrthoSize, targetOrthoSize, Time.deltaTime * zoomingSpeed);
        cinemachineCamera.Lens.OrthographicSize = currentOrthoSize;
    }
}