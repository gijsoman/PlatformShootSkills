using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public Transform Target;

	void Update ()
    {
        transform.position = Target.position + offset;

        
	}

    private void LateUpdate()
    {
        transform.RotateAround(Target.position, Vector3.up, Target.rotation.eulerAngles.y);
    }
}
