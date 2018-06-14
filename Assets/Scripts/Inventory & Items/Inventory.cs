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

    public int Space = 20;
    public List<Item> Items = new List<Item>();

    public bool Add(Item _item)
    {
        if (Items.Count >= Space)
        {
            Debug.Log("Not enough room in inventory");
            return false;
        }

        Items.Add(_item);
        if (onItemChangesCallBack != null)
        {
            onItemChangesCallBack.Invoke(_item);
        }
        return true;
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
