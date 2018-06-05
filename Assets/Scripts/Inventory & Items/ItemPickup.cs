using UnityEngine;

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

    private void Pickup()
    { 
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeMaterial(Material _focussedMaterial, Material _idleMaterial)
    {
        if (isFocused)
        {
            GetComponent<Renderer>().material = _focussedMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = _idleMaterial;
        }
    }
}
