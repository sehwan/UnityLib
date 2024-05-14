using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerDown : MonoBehaviour
, IUpdateSelectedHandler
, IPointerUpHandler, IPointerDownHandler
, IDragHandler, IEndDragHandler
{
    public UnityEvent onDown;

    public void OnUpdateSelected(BaseEventData e)
    {
        if (isPressing == false && isDragging == false) return;
        onDown.Invoke();
    }

    bool isPressing;
    public void OnPointerDown(PointerEventData e)
    {
        isPressing = true;
    }
    public void OnPointerUp(PointerEventData e) => isPressing = false;


    bool isDragging;
    public void OnDrag(PointerEventData e) => isDragging = true;
    public void OnEndDrag(PointerEventData e) => isDragging = false;
}
