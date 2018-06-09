using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    //Using a nested list instead of an array in case we want to increase the size of the inventory.
    public List<List<GameObject>> Slots = new List<List<GameObject>>();

    [SerializeField] private Vector2Int amountOfSlots = new Vector2Int(10, 10);

    private float slotWidthAndHeight;
    //[SerializeField] private int slotPadding = 10;

    private float gridStartPosition = 0;
    private float totalslotHeight;

    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        Debug.Log(rect.sizeDelta.x);
        CreateSlots();
    }

    private void CreateSlots()
    {
        for (int y = 0; y < amountOfSlots.y; y++)
        {
            List<GameObject> slotsSubList = new List<GameObject>();
            for (int x = 0; x < amountOfSlots.x; x++)
            {
                GameObject slot = new GameObject("Slot " + x + ", ");
                slot.transform.SetParent(transform);
                Slot currentSlot = slot.AddComponent<Slot>();
                //set slotWidthAndHeight based on the width of our transform.
                slotWidthAndHeight = rect.sizeDelta.x / amountOfSlots.x;
                currentSlot.transform.localScale = new Vector3(1,1,1);
                currentSlot.SetWidthAndHeight(slotWidthAndHeight, slotWidthAndHeight);
                currentSlot.SetAnchor(new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1)); //fix this with an enumerator so the user can choose the place of the anchor and of the pivot. or maybe dont let the user choose and always create the grid within the parent container.
                currentSlot.SetAnchoredPosition(gridStartPosition + slotWidthAndHeight * x, gridStartPosition - y * slotWidthAndHeight);
                slotsSubList.Add(slot);
                totalslotHeight += slotWidthAndHeight;
            }
            Slots.Add(slotsSubList);

            rect.sizeDelta = new Vector2(rect.sizeDelta.x, totalslotHeight);
        }
    }

}

