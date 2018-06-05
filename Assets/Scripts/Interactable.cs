using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isFocused = false;

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
        if (Input.GetKeyDown(KeyCode.E) && isFocused)
        {
            Interact();
            hasInteracted = true;
        }
        if (isFocused)
        {
            Debug.Log(name + " | " + "is am focused");
        }
    }

}
