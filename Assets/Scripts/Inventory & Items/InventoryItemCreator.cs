using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class InventoryItemCreator : MonoBehaviour
{
    [SerializeField] private Transform createdInventoryItemParent;
    private Inventory inventory;

    private SlotGridStorageManager slotGridManager;

    private void Start()
    {
        inventory = Inventory.instance;
        slotGridManager = SlotGridStorageManager.instance;
        inventory.onItemAddedCallBack += CreateInventoryItem;
    }

    private void CreateInventoryItem(Item _item)
    {
        GameObject inventoryItem = new GameObject(_item.name, typeof(Image), typeof(InventoryItem));
        inventoryItem.GetComponent<InventoryItem>().SetInventoryItem(_item, createdInventoryItemParent);
        slotGridManager.StoreItem(inventoryItem);
    }
}
