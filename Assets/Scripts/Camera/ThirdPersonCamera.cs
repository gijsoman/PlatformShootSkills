using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [HideInInspector] public float distanceFromTarget = 1f;

    [HideInInspector] public Vector3 currentRotation;

    [HideInInspector] public bool sameRotationAsCharacter = true;

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
