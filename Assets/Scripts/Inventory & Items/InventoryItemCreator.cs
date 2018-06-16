using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class InventoryItemCreator : MonoBehaviour
{
    [SerializeField] private Transform createdInventoryItemParent;
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangesCallBack += CreateInventoryItem;
    }

    private void CreateInventoryItem(Item _item)
    {
        GameObject inventoryItem = new GameObject(_item.name, typeof(Image), typeof(InventoryItem));
        inventoryItem.GetComponent<InventoryItem>().SetInventoryItem(_item, createdInventoryItemParent);
    }
}
