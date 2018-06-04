using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInteractable : MonoBehaviour
{
    [SerializeField] private float sphereCastRadius = 0.5f;
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private LayerMask layerMask;

    private float currentHitDistance;

    private List<GameObject> hitObjects = new List<GameObject>();

	private void Update ()
    {
        currentHitDistance = maxRayDistance;
        hitObjects.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereCastRadius, transform.forward , maxRayDistance, layerMask);
        foreach (RaycastHit hit in hits)
        {
            hitObjects.Add(hit.transform.gameObject);
            currentHitDistance = hit.distance;
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(transform.position, transform.position + Vector3.forward * currentHitDistance);
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * currentHitDistance, sphereCastRadius);
    }
}
