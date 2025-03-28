using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PointerDown : MonoBehaviour
, IUpdateSelectedHandler
, IPointerUpHandler, IPointerDownHandler
, IDragHandler, IEndDragHandler
{
    public UnityEvent cb;

    public void OnUpdateSelected(BaseEventData e)
    {
        if (isPressing == false && isDragging == false) return;
        cb.Invoke();
    }
    

    public bool isPressing;
    public void OnPointerDown(PointerEventData e)
    {
        isPressing = true;
    }
    public void OnPointerUp(PointerEventData e) => isPressing = false;


    public bool isDragging;
    public void OnDrag(PointerEventData e) => isDragging = true;
    public void OnEndDrag(PointerEventData e) => isDragging = false;
}
