using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGridManager : MonoBehaviour
{
    private void StoreItem(GameObject _item)
    {
        Slot slot;
        SlotGrid slotGrid;
        Vector2Int itemSize = _item.GetComponent<Item>().AmountOfSlotsOccupying;
        for (int y = 0; y < itemSize.y; y++)
        {
            for (int x = 0; x < itemSize.x; x++)
            {
                slot = slotgri
            }
        }
    }
}
