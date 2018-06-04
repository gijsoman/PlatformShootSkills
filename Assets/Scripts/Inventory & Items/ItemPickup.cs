using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private Item item;

    public override void Interact()
    {
        base.Interact();
        Pickup();
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
