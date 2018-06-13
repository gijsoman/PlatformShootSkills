using System.Collections.Generic;
using UnityEngine;

//TODO: 
//1. Clean up your code. Place it following your conventions.
//2. Add padding to cells.
//3. Make containerpanel mantain aspectratio when resizing. 
//4. Should I fix the hardcoded part?


public class SlotGrid : MonoBehaviour
{
    public List<List<GameObject>> Slots = new List<List<GameObject>>();

    [SerializeField] private Vector2Int amountOfSlots = new Vector2Int(10, 10);

    private float slotWidthAndHeight;
    [SerializeField] private int slotPadding = 10;

    private float gridStartPosition = 0;
    private float totalSlotHeight;

    RectTransform rect;

    private void Start()
    {
        CreateSlots();
    }

    private void OnValidate()
    {
        rect = GetComponent<RectTransform>();
        if (amountOfSlots.x != 0)
        {
            slotWidthAndHeight = rect.sizeDelta.x / amountOfSlots.x;
        }
        totalSlotHeight = amountOfSlots.y * slotWidthAndHeight + amountOfSlots.y * slotPadding;
        rect.sizeDelta = new Vector2(rect.sizeDelta.x + amountOfSlots.x * slotPadding, totalSlotHeight);
    }

    private void CreateSlots()
    {
        for (int y = 0; y < amountOfSlots.y; y++)
        {
            List<GameObject> slotsSubList = new List<GameObject>();
            for (int x = 0; x < amountOfSlots.x; x++)
            {
                GameObject slot = new GameObject("Slot " + x + ", " + y);
                slot.transform.SetParent(transform);
                Slot currentSlot = slot.AddComponent<Slot>();
                RectTransform slotRect = slot.GetComponent<RectTransform>();
                currentSlot.transform.localScale = Vector3.one;
                slotRect.sizeDelta = new Vector2(slotWidthAndHeight, slotWidthAndHeight);
                slotRect.pivot = new Vector2(0,1);
                slotRect.anchorMin = new Vector2(0,1);
                slotRect.anchorMax = new Vector2(0,1);
                slotRect.anchoredPosition = new Vector2(gridStartPosition + x * slotWidthAndHeight + slotPadding * x, gridStartPosition - y * slotWidthAndHeight - slotPadding * y);
                slotsSubList.Add(slot);
            }
            Slots.Add(slotsSubList);
        }
    }
}

