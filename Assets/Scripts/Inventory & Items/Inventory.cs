using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of the infentory found");
        }
        instance = this;
    }
    #endregion

    public int MaxSpace = 20;
    public List<Item> Items = new List<Item>();
    public delegate void OnItemAdded(Item _item);
    public delegate void OnItemRemoved(Item _item);
    public OnItemAdded OnItemAddedCallBack;
    public OnItemRemoved OnItemRemovedCallBack;
    
    [SerializeField] private int currentlyOccupiedSpace;

    private SlotGrid slotGrid;
    private SlotGridStorageManager slotGridManager;

    private void Start()
    {
        slotGrid = SlotGrid.instance;
        slotGridManager = SlotGridStorageManager.instance;
        MaxSpace = slotGrid.AmountOfSlots.x * slotGrid.AmountOfSlots.y;
    }

    public bool Add(Item _item)
    {
        if (currentlyOccupiedSpace + _item.ItemSize() >= MaxSpace)
        {
            Debug.Log("Not enough room in inventory");
            return false;
        }

        if (slotGridManager.CanWeStoreItem(_item))
        {
            Items.Add(_item);
            currentlyOccupiedSpace += _item.ItemSize();
            if (OnItemAddedCallBack != null)
            {
                OnItemAddedCallBack.Invoke(_item);
            }
            return true;
        }

        Debug.Log("Your bag is not organized enough. We can't store the item");
        return false;
    }

    public void Remove(Item _item)
    {
        Items.Remove(_item);
        if (OnItemRemovedCallBack != null)
        {
            OnItemRemovedCallBack.Invoke(_item);
        }
    }
}
