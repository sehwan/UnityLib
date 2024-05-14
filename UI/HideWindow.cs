using UnityEngine;
using UnityEngine.EventSystems;

public class HideWindow : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        var window = GetComponentInParent<UIWindow>();
        if (window == null)
        {
            Debug.Log($"<color=cyan>No Window</color>");
            return;
        }
        window.Hide();
    }
}