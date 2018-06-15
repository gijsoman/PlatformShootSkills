using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour
{
    public bool IsOccupied;

    private GameObject storedItem;
    private Vector2Int storedItemSize;
}
