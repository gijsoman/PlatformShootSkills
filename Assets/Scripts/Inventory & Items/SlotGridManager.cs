using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGridManager : MonoBehaviour
{
    #region Singleton
    public static SlotGridManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of the infentory found");
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject itemPanel;

    public void StoreItem(GameObject _item) //maybe also a location to store the item in? Location in the list?
    {
        Slot slot;
        Vector2Int amountOfSlotsOccupying = _item.GetComponent<Item>().AmountOfSlotsOccupying;
        for (int x = 0; x < amountOfSlotsOccupying.x; x++)
        {
            for (int y = 0; y < amountOfSlotsOccupying.y; y++)
            {
                //set all the slot values.
            }
        }
    }

    public void CheckForFreeSpotInGrid(Item _item)
    {
        //We want to check for a free spot to store the item. We also want to check if the neighbour slots are available. We also want to return the location of the free spot.
        SlotGrid slotGrid = itemPanel.GetComponent<SlotGrid>();
        List<bool> allNeighboursOccupied = new List<bool>();
        //First we check the first list of slots
        for (int y = 0; y < slotGrid.Slots.Count; y++)
        {
            //We store the first list in a temporary sublist.
            List<GameObject> subList = slotGrid.Slots[y];
            //Walk through each slot in this list
            for (int x = 0; x < subList.Count; x++)
            {
                if (subList[x].GetComponent<Slot>().IsOccupied)
                {
                    continue;
                }
                //We check the neighbours on this row.
                for (int i = 0; i < _item.AmountOfSlotsOccupying.x; i++)
                {
                    //Save somewhere that the slot is free.
                    if (!subList[i].GetComponent<Slot>().IsOccupied)
                    {
                        allNeighboursOccupied.Add(false);
                    }
                }
            }
        }
    }
}
