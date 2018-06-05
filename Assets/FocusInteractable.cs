using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusInteractable : MonoBehaviour
{
    [SerializeField] private float sphereCastRadius = 0.5f;
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform rayCastOrigin;

    private float lowestDistance = Mathf.Infinity;

    private bool allowedToDefocus = false;
    private bool allowedToFocus = true;

    private float currentHitDistance;

    private GameObject currentlyFocusedObject;

	private void Update ()
    {
        currentHitDistance = maxRayDistance;
        RaycastHit[] hits = Physics.SphereCastAll(rayCastOrigin.position, sphereCastRadius, transform.forward, maxRayDistance, layerMask);  
        foreach (RaycastHit hit in hits)
        {

        }
	}

    private void SetFocus()
    {
        //set the currently focused gameobject to the closest object in the hit objects.
        if (currentlyFocusedObject != null)
        {
            Interactable interactable;
            if (interactable = currentlyFocusedObject.GetComponent<Interactable>())
            {
                interactable.isFocused = true;
            }
        }

        allowedToDefocus = true;
    }

    private void DeFocus()
    {
        if (currentlyFocusedObject != null)
        {
            Interactable interactable;
            if (interactable = currentlyFocusedObject.GetComponent<Interactable>())
            {
                interactable.isFocused = false;
            }
        }
        allowedToDefocus = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(rayCastOrigin.position, rayCastOrigin.position + transform.forward * currentHitDistance);
        Gizmos.DrawWireSphere(rayCastOrigin.position + transform.forward * currentHitDistance, sphereCastRadius);
    }
}
