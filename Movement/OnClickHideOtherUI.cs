using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickHideOtherUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject target;

    public void OnPointerClick(PointerEventData e)
    {
        target.SetActive(false);
    }
}
