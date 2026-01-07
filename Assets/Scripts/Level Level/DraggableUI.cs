using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableUI : MonoBehaviour, IDragHandler
{
    private RectTransform panelRectTransform;
    private RectTransform parentRectTransform;
    private Canvas canvas;
    public Vector2 startingPosition;

    void Awake()
    {
        startingPosition = gameObject.transform.position;
    }

    void Start()
    {
        // Get the RectTransform of the panel being dragged
        panelRectTransform = GetComponent<RectTransform>();

        // Get the parent's RectTransform (usually the Canvas or a container)
        parentRectTransform = transform.parent.GetComponent<RectTransform>();

        // Find the parent canvas
        canvas = GetComponentInParent<Canvas>();

        if (panelRectTransform == null || parentRectTransform == null || canvas == null)
        {
            Debug.LogError("DraggablePanel script requires a RectTransform, a parent RectTransform, and a Canvas ancestor.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition))
        {
            // Update the panel's anchored position based on the pointer movement
            // eventData.delta is the change in position since the last frame
            panelRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

            // Clamp the new anchored position to stay within the parent boundaries
            ClampToScreen();
        }
    }

    private void ClampToScreen()
    {
        Vector2 anchoredPosition = panelRectTransform.anchoredPosition;
        Vector2 sizeDelta = panelRectTransform.sizeDelta;
        Vector2 parentSizeDelta = parentRectTransform.sizeDelta;

        // Calculate the half size of the panel and parent in local space
        float halfPanelWidth = sizeDelta.x * 0.5f;
        float halfPanelHeight = sizeDelta.y * 0.5f;
        float halfParentWidth = parentSizeDelta.x * 0.5f;
        float halfParentHeight = parentSizeDelta.y * 0.5f;

        // Calculate the boundaries relative to the parent's center (assuming default 0.5, 0.5 pivot/anchor)
        float minX = -halfParentWidth + halfPanelWidth;
        float maxX = halfParentWidth - halfPanelWidth;
        float minY = -halfParentHeight + halfPanelHeight;
        float maxY = halfParentHeight - halfPanelHeight;

        // Apply clamping
        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, minX, maxX);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, minY, maxY);

        panelRectTransform.anchoredPosition = anchoredPosition;
    }
}