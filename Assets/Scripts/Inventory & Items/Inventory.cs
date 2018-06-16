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

    public delegate void OnItemChanges(Item _item);
    public OnItemChanges onItemChangesCallBack;

    public int MaxSpace = 20;
    public List<Item> Items = new List<Item>();
    
    [SerializeField] private int currentlyOccupiedSpace;

    private SlotGrid slotGrid;
    private SlotGridManager slotGridManager;

    private void Start()
    {
        slotGrid = SlotGrid.instance;
        slotGridManager = SlotGridManager.instance;
        MaxSpace = slotGrid.amountOfSlots.x * slotGrid.amountOfSlots.y;
    }

    public bool Add(Item _item)
    {
        if (currentlyOccupiedSpace + _item.ItemSize() >= MaxSpace)
        {
            Debug.Log("Not enough room in inventory");
            return false;
        }

        //if we can store item invoke delegate and add it to the list. otherwise we return false with a warning.
        if (slotGridManager.CanWeStoreItem(_item))
        {
            Items.Add(_item);
            currentlyOccupiedSpace += _item.ItemSize();
            if (onItemChangesCallBack != null)
            {
                onItemChangesCallBack.Invoke(_item);
            }
            return true;
        }

        Debug.Log("Your bag is not organized enough. We can't store the item");
        return false;
    }

    public void Remove(Item _item)
    {
        Items.Remove(_item);
        if (onItemChangesCallBack != null)
        {
            onItemChangesCallBack.Invoke(_item);
        }
    }
}
