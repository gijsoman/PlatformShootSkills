using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private float slotSize;
    [SerializeField] private Image itemIcon;

    private SlotGrid slotGrid;
    private RectTransform rect;

    bool isDragging = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        itemIcon = GetComponent<Image>();
    }

    public void SetInventoryItem(Item _item, Transform _parent)
    {
        slotGrid = SlotGrid.instance;
        slotSize = slotGrid.slotWidthAndHeight;
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.sizeDelta = new Vector2(_item.AmountOfSlotsOccupying.x * slotSize, _item.AmountOfSlotsOccupying.y * slotSize);
        itemIcon.sprite = _item.ItemIcon;
        transform.SetParent(_parent);
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Input.mousePosition;
        }
    }
}
