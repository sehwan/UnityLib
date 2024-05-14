using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipOrPopUp : MonoBehaviour,
    // IPointerDownHandler, IPointerUpHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    [Header("Make a Method SaveTip(){TooltipOrPopup.SaveTip(,,,);} To Target")]
    public GameObject target;
    public static string title;
    public static string content;
    public static Sprite sprite;

    public static void SaveTip(string t, Sprite s, string c)
    {
        title = t;
        content = c;
        sprite = s;
    }

    // Mouse
    public void OnPointerEnter(PointerEventData e)
    {
        // #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        // ToastGroup.Show("OnPointerEnter");
        target.SendMessage("SaveTip");
        Tooltip.i.Show(title, sprite, content);
        // #endif
    }
    public void OnPointerExit(PointerEventData e)
    {
        // #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        // ToastGroup.Show("OnPointerExit");
        Tooltip.i.Hide();
        // #endif
    }

    //     // Mobile
    //     public void OnPointerUp(PointerEventData e)
    //     {
    // #if !UNITY_EDITOR && UNITY_ANDROID || UNITY_IOS 
    //         // ToastGroup.Show("OnPointerUp");
    //         SimplePopup.i.Hide();
    //         // target.SendMessage("SaveTip");
    //         // SimplePopup.i.Show(title, sprite, content);
    // #endif
    //     }
    //     public void OnPointerDown(PointerEventData e)
    //     {
    // #if !UNITY_EDITOR && UNITY_ANDROID || UNITY_IOS
    //         // ToastGroup.Show("OnPointerDown");
    //         target.SendMessage("SaveTip");
    //         SimplePopup.i.Show(title, sprite, content);
    // #endif
    //     }
}
