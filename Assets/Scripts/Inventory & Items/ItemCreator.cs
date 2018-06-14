using UnityEngine;
using UnityEngine.UI;

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
        GameObject inventoryItem = new GameObject(_item.name, typeof(Image), typeof(InventoryItem));
        Debug.Log(_item.name);
    }
}
