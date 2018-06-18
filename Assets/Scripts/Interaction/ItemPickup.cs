using UnityEngine;

public class ItemPickup : Interactable
{
    public Item ItemToPickup;

    [SerializeField] private Material focussedMaterial;
    [SerializeField] private Material idleMaterial;

    public override void Interact()
    {
        base.Interact();
        Pickup();
    }

    public override bool IsFocused()
    {
        if (Focused)
        {
            GetComponent<Renderer>().material = focussedMaterial;
            name = ItemToPickup.ItemName;
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
        bool wasPickedUp = Inventory.instance.Add(ItemToPickup);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
