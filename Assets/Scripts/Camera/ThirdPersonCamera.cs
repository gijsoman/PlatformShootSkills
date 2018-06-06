using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool SameRotationAsCharacter { get; set; }
    public float DistanceFromTarget = 1f;
    public Vector3 CurrentRotation { get; set; }

    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        if (SameRotationAsCharacter)
        {
            CurrentRotation = target.eulerAngles;
        }

        transform.eulerAngles = CurrentRotation;
        transform.position = target.position - transform.forward * DistanceFromTarget;
	}
}
