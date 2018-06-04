using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Collider interactionCollider;

    private bool hasInteracted = false;

    public virtual void Interact()
    {
        //This method is meant to be overwritten/
        if (!hasInteracted)
        {
            Debug.Log("Interacting");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interact();
        hasInteracted = true;
    }

    private void OnTriggerExit(Collider other)
    {
        hasInteracted = false;
    }
}
