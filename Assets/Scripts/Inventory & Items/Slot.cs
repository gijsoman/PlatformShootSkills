using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour
{
    private bool isOccupied;

    private GameObject storedItem;
    private Vector2Int storedItemSize;
}
