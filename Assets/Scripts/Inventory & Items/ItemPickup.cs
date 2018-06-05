﻿using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private Item item;
    [SerializeField] private Material focussedMaterial;
    [SerializeField] private Material idleMaterial;

    public override void Interact()
    {
        base.Interact();
        Pickup();
    }

    public override bool IsFocused()
    {
        if (isFocused)
        {
            GetComponent<Renderer>().material = focussedMaterial;
            return true;
        }
        else
        {
            GetComponent<Renderer>().material = idleMaterial;
            return false;
        }
    }

    private void Pickup()
    { 
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
