using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Slot : MonoBehaviour
{
    [SerializeField] private Vector2Int widthHeight = new Vector2Int(10, 10);

    private bool isOccupied;
    private RectTransform rect;

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
