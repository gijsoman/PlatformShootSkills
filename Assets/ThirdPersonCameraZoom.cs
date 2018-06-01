using UnityEngine;

[RequireComponent(typeof(ThirdPersonCamera))]
public class ThirdPersonCameraZoom : MonoBehaviour
{
    [SerializeField] private float minimumZoom = 1f;
    [SerializeField] private float maximumZoom = 10f;
    [SerializeField] private float zoomSpeed = 1f;

    private ThirdPersonCamera thirdPersonCamera;

    private void Start()
    {
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
    }

}
