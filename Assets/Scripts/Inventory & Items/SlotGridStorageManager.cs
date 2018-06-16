using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlotGrid))]
public class SlotGridStorageManager : MonoBehaviour
{
    #region Singleton
    public static SlotGridStorageManager instance;

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
    [SerializeField] private Transform storedItemsParent;

    private SlotGrid slotGrid;

    private void Start()
    {
        slotGrid = GetComponent<SlotGrid>();
    }

    public bool CanWeStoreItem(Item _itemToCheck)
    {
        if (FindSlotToStoreItem(_itemToCheck) != null)
        {
            return true;
        }
        return false;
    }

    public void StoreItem(GameObject _inventoryItem) 
    {
        Item itemTryingToStore = _inventoryItem.gameObject.GetComponent<InventoryItem>().item;
        //I want a list of all slots we can store store the item in......
        Debug.Log("We can store the item");
        //Store the actual item
        for (int i = 0; i < currentSlotsArea.Count; i++)
        {
            currentSlotsArea[i].IsOccupied = true;
            currentSlotsArea[i].storedItem = _inventoryItem;
        }
        //change the position, parent and the pivot of the item.
        RectTransform rect = _inventoryItem.GetComponent<RectTransform>();
        rect.pivot = new Vector2(0, 1);
        _inventoryItem.transform.position = currentSlotsArea[0].transform.position;
        _inventoryItem.transform.SetParent(storedItemsParent);
        Debug.Log("We can not store the item");
    }

    public List<Slot> FindSlotToStoreItem(Item _itemTryingToStore)
    {
        currentSlotsArea.Clear();
        //We want to check for a free spot to store the item. We also want to check if the neighbour slots are available. We also want to return the location of the free spot.
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
                    if (!CheckForEndOfGridReached(currentSlot, _itemTryingToStore))
                    {
                        //Get all the neighbours of this slot if it it isn't occupied.
                        if (IsEverySlotInAreaFree(GetSlotsArea(currentSlot, _itemTryingToStore)))
                        {
                            return currentSlotsArea;
                        }
                    }
                }
            }
        }

        return null;
    }

    private bool IsEverySlotInAreaFree(List<Slot> _slotsArea)
    {
        for (int i = 0; i < _slotsArea.Count; i++)
        {
            if (_slotsArea[i].IsOccupied)
            {
                Debug.Log("Not every slot is free");
                return false;
            }
        }
        
        return true;
    }

    private List<Slot> GetSlotsArea(Slot _currentSlot, Item _itemTryingToAdd)
    {
        currentSlotsArea.Clear();
        for (int y = _currentSlot.positionInGrid.y; y < _currentSlot.positionInGrid.y + _itemTryingToAdd.AmountOfSlotsOccupying.y; y++)
        {
            List<GameObject> subList = slotGrid.Slots[y];
            for (int x = _currentSlot.positionInGrid.x; x < _currentSlot.positionInGrid.x + _itemTryingToAdd.AmountOfSlotsOccupying.x; x++)
            {
                Slot currentSlot = subList[x].GetComponent<Slot>();
                currentSlotsArea.Add(currentSlot);
            }
        }

        return currentSlotsArea;
    }

    private bool CheckForEndOfGridReached(Slot _currentSlot, Item _itemTryingToAdd)
    {
        //check if the item is not to big for the remaining slots. 
        int amountOfYSlotsLeft = 0;
        int amountOfXSlotsLeft = 0;

        for (int y = _currentSlot.positionInGrid.y; y < slotGrid.Slots.Count; y++)
        {
            amountOfYSlotsLeft++;
        }

        List<GameObject> xSlots = slotGrid.Slots[_currentSlot.positionInGrid.y];
        for (int x = _currentSlot.positionInGrid.x; x < xSlots.Count; x++)
        {
            amountOfXSlotsLeft++;
        }

        if (amountOfYSlotsLeft < _itemTryingToAdd.AmountOfSlotsOccupying.y)
        {
            return true;
        }
        if (amountOfXSlotsLeft < _itemTryingToAdd.AmountOfSlotsOccupying.x)
        {
            return true;
        }

        return false;
    }
}
