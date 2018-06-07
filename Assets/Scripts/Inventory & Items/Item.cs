using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string ItemName = "New Item";
    public Sprite ItemIcon = null;
    public Vector2Int AmountOfSlotsOccupying = new Vector2Int(1,1);
}
