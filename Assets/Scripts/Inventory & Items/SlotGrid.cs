using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    //Using a nested list instead of an array in case we want to increase the size of the inventory.
    public List<List<GameObject>> Slots = new List<List<GameObject>>();

    [SerializeField] private Vector2Int amountOfSlots = new Vector2Int(10, 10);

    private float slotWidthAndHeight;
    //ADD SLOT PADDING NOW!
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
                //set slotWidthAndHeight based on the width of our transform.
                currentSlot.transform.localScale = new Vector3(1,1,1);
                currentSlot.SetWidthAndHeight(slotWidthAndHeight, slotWidthAndHeight);
                currentSlot.SetAnchor(new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1)); //fix this with an enumerator so the user can choose the place of the anchor and of the pivot. or maybe dont let the user choose and always create the grid within the parent container.
                currentSlot.SetAnchoredPosition(gridStartPosition + slotWidthAndHeight * x, gridStartPosition - y * slotWidthAndHeight);
                slotsSubList.Add(slot);
            }
            Slots.Add(slotsSubList);
        }
    }
}

