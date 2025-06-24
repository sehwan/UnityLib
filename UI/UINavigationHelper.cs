using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINavigationHelper : MonoBehaviour, ISelectHandler
{
    Navigation navi;

    void Awake()
    {
        navi = GetComponent<Selectable>().navigation;
    }
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log($"Up: {navi.selectOnUp}");
        Debug.Log($"Down: {navi.selectOnDown}");
        Debug.Log($"Left: {navi.selectOnLeft}");
        Debug.Log($"Right: {navi.selectOnRight}");
    }
}
