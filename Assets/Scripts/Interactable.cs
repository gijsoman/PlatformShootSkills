using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isFocused = false;

    [SerializeField] private Collider interactionCollider;

    private bool mayInteract = false;
    private bool hasInteracted = false;

    public virtual void Interact()
    {
        //This method is meant to be overwritten.
        if (!hasInteracted)
        {
            Debug.Log("Interacting");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && mayInteract && isFocused)
        {
            Interact();
            hasInteracted = true;
        }
        if (isFocused)
        {
            Debug.Log(name + " | " + "is am focused");
        }
    }

    private void ChangeMaterial()
    {

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
