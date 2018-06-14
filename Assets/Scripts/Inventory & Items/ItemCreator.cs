using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ItemCreator : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangesCallBack += CreateItem;
    }

    private void CreateItem(Item _item)
    {
        
    }
}
