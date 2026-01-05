using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickEvents : MonoBehaviour, IPointerClickHandler
{
     [SerializeField] private UnityEvent leftClick;
     [SerializeField] private UnityEvent rightClick;
    [SerializeField] private UnityEvent middleClick;

    // This method is called by the EventSystem when a pointer click occurs on this UI element.
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check which mouse button was clicked using the InputButton enum
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            leftClick.Invoke();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            rightClick.Invoke();
        }
        // Middle button can also be checked if needed
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            middleClick.Invoke();
        }
    }
}