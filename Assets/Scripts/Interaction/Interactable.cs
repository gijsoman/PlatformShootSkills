using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool Focused = false;

    private bool hasInteracted = false;

    public virtual void Interact()
    {
       
    }

    public virtual bool IsFocused()
    {
        if (Focused)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Focused)
        {
            Interact();
            hasInteracted = true;
        }

        IsFocused();
    }

}
