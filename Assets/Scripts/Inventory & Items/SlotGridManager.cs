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
    [SerializeField] private List<Slot> currentSlotsArea = new List<Slot>();

    private SlotGrid slotGrid;

    private void Start()
    {
        slotGrid = itemPanel.GetComponent<SlotGrid>();
    }

    public void StoreItem(GameObject _item) //maybe also a location to store the item in? Location in the list?
    {
        Item itemTryingToStore = _item.GetComponent<Item>();

    }

    public Slot CheckForFreeSlotInGrid(Item _itemTryingToStore)
    {
        //We want to check for a free spot to store the item. We also want to check if the neighbour slots are available. We also want to return the location of the free spot.
        //First we check the first list of slots
        for (int y = 0; y < slotGrid.Slots.Count; y++)
        {
            //We store the first list in a temporary sublist.
            List<GameObject> subList = slotGrid.Slots[y];
            //Walk through each slot in this list
            for (int x = 0; x < subList.Count; x++)
            {
                Slot currentSlot = subList[x].GetComponent<Slot>();
                if (!currentSlot.IsOccupied)
                {
                    //Get all the neighbours of this slot if it it isn't occupied.
                    if (IsEverySlotInAreaFree(GetSlotsArea(currentSlot, _itemTryingToStore)))
                    {
                        return currentSlot;
                    }
                }
            }
        }
    }

    private bool IsEverySlotInAreaFree(List<Slot> _slotsArea)
    {
        for (int i = 0; i < _slotsArea.Count; i++)
        {
            if (_slotsArea[i].IsOccupied)
            {
                return false;
            }
        }
        return true;
    }

    private List<Slot> GetSlotsArea(Slot _currentSlot, Item _itemTryingToAdd)
    {
        for (int y = _currentSlot.positionInGrid.y; y < _itemTryingToAdd.AmountOfSlotsOccupying.y; y++)
        {
            List<GameObject> subList = slotGrid.Slots[y];
            for (int x = _currentSlot.positionInGrid.x; x < _itemTryingToAdd.AmountOfSlotsOccupying.x; x++)
            {
                Slot currentSlot = subList[x].GetComponent<Slot>();
                if (!currentSlot.IsOccupied)
                {
                    currentSlotsArea.Add(currentSlot);
                }
            }
        }

        return currentSlotsArea;
    }
}
