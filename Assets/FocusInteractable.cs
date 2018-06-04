using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInteractable : MonoBehaviour
{
    [SerializeField] private float sphereCastRadius = 0.5f;
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform rayCastOrigin;

    private float currentHitDistance;

    private List<GameObject> hitObjects = new List<GameObject>();

	private void Update ()
    {
        currentHitDistance = maxRayDistance;
        hitObjects.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(rayCastOrigin.position, sphereCastRadius, transform.forward, maxRayDistance, layerMask);
        foreach (RaycastHit hit in hits)
        {
            hitObjects.Add(hit.transform.gameObject);
            Debug.Log(hit.transform.name);
            currentHitDistance = hit.distance;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(rayCastOrigin.position, rayCastOrigin.position + transform.forward * currentHitDistance);
        Gizmos.DrawWireSphere(rayCastOrigin.position + transform.forward * currentHitDistance, sphereCastRadius);
    }
}
