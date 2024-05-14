using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MakeThisSimpleTooltip : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tip;
    public float time = 5f;


    // void Start()
    // {
    //     var btn = GetComponent<Button>();
    //     btn.onClick.RemoveAllListeners();
    //     btn.onClick.AddListener(ShowTooltip);
    // }
    void ShowTooltip()
    {
        SimplePopup.i.Show(null, null, tip.L(), time);
        // Tooltip.i.Show(tip.L(), null, null);
    }

    public void OnPointerEnter(PointerEventData e)
    {
        // target.SendMessage("SaveTip");
        Tooltip.i.Show(tip.L(), null, null);
    }
    public void OnPointerExit(PointerEventData e)
    {
        Tooltip.i.Hide();
    }
}
