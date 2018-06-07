using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int amountOfSlots = new Vector2Int(10, 10);
    [SerializeField] private Vector2Int slotSizes = new Vector2Int(10, 10);

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
            for (int X = 0; X < amountOfSlots.x; X++)
            {
                GameObject slot = new GameObject();
                slot.transform.SetParent(transform);
                slot.AddComponent<Slot>();
                slotsSubList.Add(slot);
            }
            slots.Add(slotsSubList);
        }
    }
}


