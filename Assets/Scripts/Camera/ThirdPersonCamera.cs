using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool SameRotationAsCharacter;
    public float Pitch { get; set; }
    public float Yaw { get; set; }
    public float DistanceFromTarget = 1f;

    [SerializeField] private Transform target;
    [SerializeField] private float pitchMin = -40;
    [SerializeField] private float pitchMax = 80;
    [SerializeField] private float rotationSmoothTime = 0.05f;

    private Vector3 rotationVelocity;
    private Vector3 currentRotation;

    private void LateUpdate()
    {
        if (SameRotationAsCharacter)
        {
            currentRotation = target.eulerAngles;
        }
        else
        {
            Pitch = Mathf.Clamp(Pitch, pitchMin, pitchMax);
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(Pitch, Yaw), ref rotationVelocity, rotationSmoothTime);
        }

        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * DistanceFromTarget;
    }
}
