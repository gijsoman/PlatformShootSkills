using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
public class Slot : MonoBehaviour
{
    [SerializeField] private Vector2Int widthHeight = new Vector2Int(10, 10);

    private bool isOccupied;
    private RectTransform rect;

    public Slot(int _width, int _height)
    {
        widthHeight.x = _width;
        widthHeight.y = _height;
    }

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        SetWidthAndHeight(widthHeight.x, widthHeight.y);
    }

    public void SetWidthAndHeight(int _width, int _height)
    {
        rect.sizeDelta = new Vector2Int(_width, _height);
    }
}
