using UnityEngine;

[RequireComponent(typeof(ThirdPersonCamera))]
public class ThirdPersonCameraMouseControl : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private bool invertAxis = false;
    [SerializeField] private float pitchMin = -40f;
    [SerializeField] private float pitchMax = 85f;
    [SerializeField] private float rotationSmoothTime = 0.05f;
    [SerializeField] private bool hideCursor = false;

    private float yaw;
    private float pitch;

    private Vector3 currentRotation;
    private Vector3 rotationVelocity;

    private ThirdPersonCamera thirdPersonCamera;

    void Start ()
    {
        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
        thirdPersonCamera.sameRotationAsCharacter = false;
    }

    void Update ()
    {
        if (!invertAxis)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        }
        else
        {
            yaw -= Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationVelocity, rotationSmoothTime);

        thirdPersonCamera.currentRotation = currentRotation;
    }
}
