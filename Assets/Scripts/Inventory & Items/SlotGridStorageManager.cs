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
        slotGrid = SlotGrid.instance;
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
        for (int i = 0; i < currentSlotsArea.Count; i++)
        {
            currentSlotsArea[i].IsOccupied = true;
            currentSlotsArea[i].storedItem = _inventoryItem;
        }

        RectTransform rect = _inventoryItem.GetComponent<RectTransform>();
        rect.pivot = new Vector2(0, 1);
        _inventoryItem.transform.position = currentSlotsArea[0].transform.position;
        _inventoryItem.transform.SetParent(storedItemsParent);
    }

    public List<Slot> FindSlotToStoreItem(Item _itemTryingToStore)
    {
        currentSlotsArea.Clear();
        for (int y = 0; y < slotGrid.Slots.Count; y++)
        {
            List<GameObject> subList = slotGrid.Slots[y];
            for (int x = 0; x < subList.Count; x++)
            {
                Slot currentSlot = subList[x].GetComponent<Slot>();
                if (!currentSlot.IsOccupied)
                {
                    if (!CheckForEndOfGridReached(currentSlot, _itemTryingToStore))
                    {
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
