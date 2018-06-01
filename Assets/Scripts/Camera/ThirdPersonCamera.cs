using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [HideInInspector] public Vector3 currentRotation;

    [HideInInspector] public bool sameRotationAsCharacter = true;

    public float distanceFromTarget = 1f;

    private void LateUpdate()
    {
        if (sameRotationAsCharacter)
        {
            currentRotation = target.eulerAngles;
        }

        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * distanceFromTarget;
	}
}
