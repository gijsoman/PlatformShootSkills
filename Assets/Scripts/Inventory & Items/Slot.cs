using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour
{
    public bool IsOccupied;

    public Vector2Int positionInGrid;
    public GameObject storedItem;
}
