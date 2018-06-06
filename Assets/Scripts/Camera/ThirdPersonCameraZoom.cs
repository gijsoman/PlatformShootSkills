using UnityEngine;

[RequireComponent(typeof(ThirdPersonCamera))]
public class ThirdPersonCameraZoom : MonoBehaviour
{
    [SerializeField] private float minimumZoom = 1f;
    [SerializeField] private float maximumZoom = 10f;
    [SerializeField] private float zoomSpeed = 1f;

    private float currentZoom;

    private float distanceFromTarget;

    private ThirdPersonCamera thirdPersonCamera;

    private void Start()
    {
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        distanceFromTarget = thirdPersonCamera.DistanceFromTarget;
    }

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minimumZoom, maximumZoom);

        thirdPersonCamera.DistanceFromTarget = distanceFromTarget * currentZoom;
    }
}
