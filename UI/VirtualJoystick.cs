using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector2 boundaryDefaultPos;
    Vector2 originPos;
    public Vector2 dir;
    public bool isTouching = false;

    [Header("Ref")]
    public RectTransform boundary;
    public RectTransform stick;

    [Header("Option")]
    public bool isMobileOnly = true;
    public float radius = 100;
    public UnityEvent<Vector2> onDrag;
    public UnityEvent<Vector2> onEndDrag;
    public UnityEvent<Vector2> onUpdate;


    void Awake()
    {
        if (isMobileOnly)
            gameObject.SetActive(Application.isMobilePlatform);
        boundaryDefaultPos = boundary.anchoredPosition;
    }
    void Update()
    {
        onUpdate?.Invoke(dir);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isTouching = true;
        boundary.position = UM.i.cam.ScreenToWorldPoint(eventData.position);
        originPos = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        isTouching = true;
        var moved = eventData.position - originPos;
        var distance = moved.magnitude;
        dir = moved / distance;
        stick.anchoredPosition = dir * radius;
        onDrag?.Invoke(dir);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isTouching = false;
        dir = Vector2.zero;
        stick.anchoredPosition = new Vector2(0.5f, 0.5f);
        boundary.anchoredPosition = boundaryDefaultPos;
        onEndDrag?.Invoke(dir);
    }

    // For Overlay Screen Space
    // public void OnDrag(PointerEventData eventData)
    // {
    //     isTouching = true;
    //     RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //         boundary,
    //         eventData.position,
    //         eventData.pressEventCamera,
    //         out Vector2 newPos);
    //     var moveDir = newPos - originPos;
    //     var distance = moveDir.magnitude;
    //     dir = moveDir / distance;
    //     if (distance > radius)
    //     {
    //         distance = radius;
    //         newPos = originPos + dir * distance;
    //     }
    //     else dir *= distance / radius;
    //     stick.anchoredPosition = newPos;
    //     onDrag?.Invoke(dir);
    // }
}