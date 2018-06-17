using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item ContainingItem;

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
        ContainingItem = _item;
        slotGrid = SlotGrid.instance;
        slotSize = slotGrid.SlotWidthAndHeight;
        transform.SetParent(_parent);
        ScaleInventoryItem(_item);
        itemIcon.sprite = _item.ItemIcon;
    }

    public void ScaleInventoryItem(Item _item)
    {
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.sizeDelta = new Vector2(_item.AmountOfSlotsOccupying.x * slotSize, _item.AmountOfSlotsOccupying.y * slotSize);
        rect.localScale = Vector3.one;
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Input.mousePosition;
        }
    }
}
