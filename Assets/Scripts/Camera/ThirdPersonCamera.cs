using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private bool invertAxis = false;
    [SerializeField] private Transform target;
    //[SerializeField] private float minZoom = 1;
    //[SerializeField] private float maxZoom = 10;
    //[SerializeField] private float zoomSpeed = 1;
    [SerializeField] private float pitchMin = 40;
    [SerializeField] private float pitchMax = 85;
    [SerializeField] private float rotationSmoothTime = 0.12f;
    [SerializeField] private bool hideCursor = false;

    public float distanceFromTarget = 1f;

    //private float zoom = 1f;
    private float yaw;
    private float pitch;

    private Vector3 currentRotation;
    private Vector3 rotationVelocity;

    private void Start()
    {
        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LateUpdate()
    {
        //zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        //zoom = Mathf.Clamp(zoom, minZoom, maxZoom);

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

        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
	}
}
