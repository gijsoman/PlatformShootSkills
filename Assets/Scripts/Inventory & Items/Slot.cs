using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour
{
    [SerializeField] private Vector2Int widthHeight = new Vector2Int(10, 10);

    private RectTransform rect;

    private bool isOccupied;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetWidthAndHeight(int _width, int _height)
    {
        rect.sizeDelta = new Vector2Int(_width, _height);
    }

    public void SetWidthAndHeight(float _width, float _height)
    {
        rect.sizeDelta = new Vector2(_width, _height);
    }

    public void SetAnchoredPosition(int _xPosition, int _yPosition)
    {
        rect.anchoredPosition = new Vector2(_xPosition, _yPosition);
    }

    public void SetAnchoredPosition(float _xPosition, float _yPosition)
    {
        rect.anchoredPosition = new Vector2(_xPosition, _yPosition);
    }

    public void SetAnchor(Vector2 _minXY, Vector2 _maxXY, Vector2 _pivot)
    {
        rect.pivot = _pivot;
        rect.anchorMin = _minXY;
        rect.anchorMax = _maxXY;
    }
}
