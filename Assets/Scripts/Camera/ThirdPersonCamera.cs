using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private bool invertAxis = false;
    [SerializeField] private float distanceFromTarget = 1f;
    [SerializeField] private Transform target;
    [SerializeField] private float minZoom = 1;
    [SerializeField] private float maxZoom = 10;

    private float zoom = 10f;
    private float yaw;
    private float pitch;

    void Update()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel");
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);

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

        Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * distanceFromTarget * zoom;
	}
}
