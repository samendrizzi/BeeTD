using UnityEngine;
using System.Collections.Generic;

public class UIParentRelativeScaler : MonoBehaviour
{
    [System.Serializable]
    public class UIElementData
    {
        public RectTransform rect;
        public RectTransform parent;

        public Vector2 positionPercent;
        public Vector2 sizePercent;
    }

    private List<UIElementData> elements = new List<UIElementData>();

    void Start()
    {
        CacheElements();
        ApplyScaling();
    }

    void CacheElements()
    {
        elements.Clear();

        RectTransform[] rects = GetComponentsInChildren<RectTransform>(true);

        foreach (RectTransform r in rects)
        {
            if (r == transform) continue;
            if (r.parent == null) continue;

            RectTransform parentRect = r.parent as RectTransform;
            if (parentRect == null) continue;

            // Skip stretched anchors (still unsafe)
            if (r.anchorMin != r.anchorMax)
                continue;

            Vector2 parentSize = parentRect.rect.size;

            // Prevent division by zero
            if (parentSize.x == 0 || parentSize.y == 0)
                continue;

            UIElementData data = new UIElementData();
            data.rect = r;
            data.parent = parentRect;

            data.positionPercent = new Vector2(
                r.anchoredPosition.x / parentSize.x,
                r.anchoredPosition.y / parentSize.y
            );

            data.sizePercent = new Vector2(
                r.sizeDelta.x / parentSize.x,
                r.sizeDelta.y / parentSize.y
            );

            elements.Add(data);
        }
    }

    void ApplyScaling()
    {
        foreach (var e in elements)
        {
            if (e.rect == null || e.parent == null) continue;

            Vector2 parentSize = e.parent.rect.size;

            e.rect.anchoredPosition = new Vector2(
                e.positionPercent.x * parentSize.x,
                e.positionPercent.y * parentSize.y
            );

            e.rect.sizeDelta = new Vector2(
                e.sizePercent.x * parentSize.x,
                e.sizePercent.y * parentSize.y
            );
        }
    }
}