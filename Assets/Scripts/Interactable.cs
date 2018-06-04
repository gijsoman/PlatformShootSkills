using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private float radius = 3f;
    [SerializeField] private Transform placeOfInteraction;
    [SerializeField] private Transform player;

    private bool hasInteracted = false;

    private void Interact()
    {
        if (!hasInteracted)
        {
            Debug.Log("Interacting");
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(placeOfInteraction.position, player.position);
        if (distance <= radius)
        {
            Interact();
            hasInteracted = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(placeOfInteraction.position, radius);
    }
}
