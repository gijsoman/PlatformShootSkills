using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Collider interactionCollider;

    public bool isFocus;

    private bool mayInteract = false;
    private bool hasInteracted = false;

    public virtual void Interact()
    {
        //This method is meant to be overwritten/
        if (!hasInteracted)
        {
            Debug.Log("Interacting");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && mayInteract)
        {
            Interact();
            hasInteracted = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        mayInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        mayInteract = false;
    }
}
