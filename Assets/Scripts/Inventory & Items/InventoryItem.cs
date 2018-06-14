using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private Item item;
    [SerializeField]private SlotGrid slotGrid;
    bool isDragging = false;

    private float slotSize;
    private RectTransform rect;
    private Sprite itemIcon;

    private void Start()
    {
        slotSize = slotGrid.slotWidthAndHeight;
        rect = GetComponent<RectTransform>();
        itemIcon = GetComponent<Sprite>();
    }

    public void SetInventoryItem(Item _item)
    {
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, item.AmountOfSlotsOccupying.x * slotSize); //Rmmbr padding needs to be added in the calculation.
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, item.AmountOfSlotsOccupying.y * slotSize); //Rmmbr padding needs to be added in the calculation.
        item = _item;
        itemIcon = _item.ItemIcon;
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Input.mousePosition;
        }
    }

}
