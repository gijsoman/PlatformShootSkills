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

    private float currentHitDistance;

    private GameObject currentlyFocused;

    //private List<GameObject> hitObjects = new List<GameObject>();

	private void Update ()
    {
        currentHitDistance = maxRayDistance;
        //hitObjects.Clear();
        RaycastHit[] hits = Physics.SphereCastAll(rayCastOrigin.position, sphereCastRadius, transform.forward, maxRayDistance, layerMask);
        foreach (RaycastHit hit in hits)
        {
            //hitObjects.Add(hit.transform.gameObject);
            currentHitDistance = hit.distance;
            if (currentHitDistance < lowestDistance)
            {
                lowestDistance = hit.distance;
                currentlyFocused = hit.transform.gameObject;
                Debug.Log(currentlyFocused.name);
            }
        }

        lowestDistance = Mathf.Infinity;
        //get the object with the lowest hit distance in the array.

        SetFocus();
	}

    private void SetFocus()
    {
        //set the currently focused gameobject to the closest object in the hit objects.
        if (currentlyFocused != null)
        {
            Interactable interactable;
            if (interactable = currentlyFocused.GetComponent<Interactable>())
            {
                interactable.isFocused = true;
            }
        }
    }

    private void DeFocus()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(rayCastOrigin.position, rayCastOrigin.position + transform.forward * currentHitDistance);
        Gizmos.DrawWireSphere(rayCastOrigin.position + transform.forward * currentHitDistance, sphereCastRadius);
    }
}
