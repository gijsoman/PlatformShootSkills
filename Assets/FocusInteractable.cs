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

    private float shortestDistance = Mathf.Infinity;

    private Interactable closestObject;

	private void Update ()
    {
        if (closestObject != null)
        {
            closestObject.Focused = false;
            closestObject = null;
        }

        currentHitDistance = maxRayDistance;

        RaycastHit[] hits = Physics.SphereCastAll(rayCastOrigin.position, sphereCastRadius, transform.forward, maxRayDistance, layerMask);  
        foreach (RaycastHit hit in hits)
        {
            currentHitDistance = hit.distance;
            Debug.DrawLine(rayCastOrigin.position, hit.transform.position, Color.red);
            if (currentHitDistance < shortestDistance)
            {
                shortestDistance = currentHitDistance;
                if (closestObject != null)
                {
                    closestObject.Focused = false;
                }
                if(closestObject = hit.transform.gameObject.GetComponent<Interactable>())
                {
                    closestObject.Focused = true;
                }
            }
        }
        shortestDistance = Mathf.Infinity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(rayCastOrigin.position, rayCastOrigin.position + transform.forward * currentHitDistance);
        Gizmos.DrawWireSphere(rayCastOrigin.position + transform.forward * currentHitDistance, sphereCastRadius);
    }
}
