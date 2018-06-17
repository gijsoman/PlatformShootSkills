using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotGrid : MonoBehaviour
{
    #region Singleton
    public static SlotGrid instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of the infentory found");
        }
        instance = this;
    }
    #endregion

    public float SlotWidthAndHeight = 20;
    public List<List<GameObject>> Slots = new List<List<GameObject>>();
    public Vector2Int AmountOfSlots = new Vector2Int(10, 10);

    [SerializeField] private int slotPadding = 10;

    private RectTransform rect;
    private AspectRatioFitter aspectFitter;

    private float gridStartPosition = 0;
    private float totalSlotHeight;
    private float totalSlotWidth;

    private void OnValidate()
    {
        rect = GetComponent<RectTransform>();
        totalSlotHeight = AmountOfSlots.y * SlotWidthAndHeight + (AmountOfSlots.y - 1) * slotPadding;
        totalSlotWidth = AmountOfSlots.x * SlotWidthAndHeight + (AmountOfSlots.x - 1) * slotPadding;
        rect.sizeDelta = new Vector2(totalSlotWidth, totalSlotHeight);
    }

    private void Start()
    {
        CreateSlots();
    }

    private void CreateSlots()
    {
        for (int y = 0; y < AmountOfSlots.y; y++)
        {
            List<GameObject> slotsSubList = new List<GameObject>();
            for (int x = 0; x < AmountOfSlots.x; x++)
            {
                GameObject slot = new GameObject("Slot " + x + ", " + y);
                
                slot.transform.SetParent(transform);
                Slot currentSlot = slot.AddComponent<Slot>();
                RectTransform slotRect = slot.GetComponent<RectTransform>();
                currentSlot.positionInGrid = new Vector2Int(x, y);
                currentSlot.transform.localScale = Vector3.one;
                slotRect.sizeDelta = new Vector2(SlotWidthAndHeight, SlotWidthAndHeight);
                slotRect.pivot = new Vector2(0,1);
                slotRect.anchorMin = new Vector2(0,1);
                slotRect.anchorMax = new Vector2(0,1);
                slotRect.anchoredPosition = new Vector2(gridStartPosition + x * SlotWidthAndHeight + slotPadding * x, gridStartPosition - y * SlotWidthAndHeight - slotPadding * y);
                slotsSubList.Add(slot);
            }
            Slots.Add(slotsSubList);
        }
    }
}

