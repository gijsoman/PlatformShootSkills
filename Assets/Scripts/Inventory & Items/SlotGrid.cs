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
    //[SerializeField] private int slotPadding = 10;

    private float gridStartPosition = 0;
    private float totalSlotHeight;

    RectTransform rect;

    private void Start()
    {
        Debug.Log(rect.sizeDelta.x);
        CreateSlots();
    }

    private void OnValidate()
    {
        rect = GetComponent<RectTransform>();
        Debug.Log("slotWidthAndHeight: " + slotWidthAndHeight);
        Debug.Log("amountOfSlots X: " + amountOfSlots.x);
        if (amountOfSlots.x != 0)
        {
            slotWidthAndHeight = rect.sizeDelta.x / amountOfSlots.x;
        }
        totalSlotHeight = amountOfSlots.y * slotWidthAndHeight;
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, totalSlotHeight);
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
                currentSlot.transform.localScale = Vector3.one;
                currentSlot.SetWidthAndHeight(slotWidthAndHeight, slotWidthAndHeight);
                currentSlot.SetAnchor(new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1));
                currentSlot.SetAnchoredPosition(gridStartPosition + slotWidthAndHeight * x, gridStartPosition - y * slotWidthAndHeight);
                slotsSubList.Add(slot);
            }
            Slots.Add(slotsSubList);
        }
    }
}

