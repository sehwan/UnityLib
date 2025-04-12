using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// [RequireComponent(typeof(Button))]
public class MakeThisSimpleTooltip : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [TextArea]
    public string tip;
    // public float time = 5f;


    // void Start()
    // {
    //     var btn = GetComponent<Button>();
    //     btn.onClick.RemoveAllListeners();
    //     btn.onClick.AddListener(ShowTooltip);
    // }
    // void ShowTooltip()
    // {
    //     SimplePopup.i.Show(null, null, tip.L(), time);
    //     // Tooltip.i.Show(tip.L(), null, null);
    // }

    public void OnPointerEnter(PointerEventData e)
    {
        // target.SendMessage("SaveTip");
        /* await  */
        Tooltip.i.Show(null, null, tip.L());
    }
    public void OnPointerExit(PointerEventData e)
    {
        Tooltip.i.Hide();
    }
    public void OnPointerUp(PointerEventData e)
    {
        Tooltip.i.Hide();
    }
}
