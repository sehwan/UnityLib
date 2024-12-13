using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool wasTooltipActive = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        wasTooltipActive = Tooltip.i.gameObject.activeSelf;
        Tooltip.i.Hide();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (wasTooltipActive) Tooltip.i.gameObject.SetActive(true);
        wasTooltipActive = false;
    }
}
