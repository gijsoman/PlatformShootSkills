using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class InventoryItemCreator : MonoBehaviour
{
    [SerializeField] private Transform createdInventoryItemParent;
    private Inventory inventory;

    private SlotGridManager slotGridManager;

    private void Start()
    {
        inventory = Inventory.instance;
        slotGridManager = SlotGridManager.instance;
        inventory.onItemChangesCallBack += CreateItem;
    }

    private void CreateItem(Item _item)
    {
        GameObject inventoryItem = new GameObject(_item.name, typeof(Image), typeof(InventoryItem));
        inventoryItem.GetComponent<InventoryItem>().SetInventoryItem(_item, createdInventoryItemParent);
        slotGridManager.StoreItem(inventoryItem);
    }
}
