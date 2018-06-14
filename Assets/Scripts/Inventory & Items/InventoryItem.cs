using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    private Item item;
    private SlotGrid slotGrid;
    bool isDragging = false;

    private float slotSize;
    private RectTransform rect;

    private void Start()
    {
        slotSize = slotGrid.slotWidthAndHeight;
        rect = GetComponent<RectTransform>();
    }

    public void CreateInventoryItem(Item _item)
    {
      
    }

}
