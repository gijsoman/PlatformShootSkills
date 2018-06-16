using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //We could also make a base class for inventory so we can create different types of inventories OR we could make an interface that ads inventory functionality..... OR We could make components that add fucntionality for differnt inventory types.

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

    public delegate void OnItemAdded(Item _item);
    public OnItemAdded onItemAddedCallBack;

    public delegate void OnItemRemoved(Item _item);
    public OnItemRemoved onItemRemovedCallBack;

    public int MaxSpace = 20;
    public List<Item> Items = new List<Item>();
    
    [SerializeField] private int currentlyOccupiedSpace;

    private SlotGrid slotGrid;
    private SlotGridStorageManager slotGridManager;

    private void Start()
    {
        slotGrid = SlotGrid.instance;
        slotGridManager = SlotGridStorageManager.instance;
        MaxSpace = slotGrid.amountOfSlots.x * slotGrid.amountOfSlots.y;
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
            if (onItemAddedCallBack != null)
            {
                onItemAddedCallBack.Invoke(_item);
            }
            return true;
        }

        Debug.Log("Your bag is not organized enough. We can't store the item");
        return false;
    }

    public void Remove(Item _item)
    {
        Items.Remove(_item);
        if (onItemRemovedCallBack != null)
        {
            onItemRemovedCallBack.Invoke(_item);
        }
    }
}
