using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int amountOfSlots = new Vector2Int(10, 10);
    [SerializeField] private Vector2Int slotSizes = new Vector2Int(10, 10);
    [SerializeField] private int slotPadding = 10;

    private int gridStartPosition = 0;

    //Using a nested list instead of an array in case we want to increase the size of the inventory.
    public List<List<GameObject>> slots = new List<List<GameObject>>();

    private void Start()
    {
        CreateSlots();
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
                currentSlot.SetWidthAndHeight(slotSizes.x, slotSizes.y);
                currentSlot.SetAnchor(new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1)); //fix this with an enumerator so the user can choose the place of the anchor and of the pivot. or maybe dont let the user choose and always create the grid within the parent container.
                currentSlot.SetAnchoredPosition(gridStartPosition + slotSizes.x * x + slotPadding * x, gridStartPosition - y * slotSizes.y - slotPadding * y);
                slotsSubList.Add(slot);
            }
            slots.Add(slotsSubList);
        }
    }

}


