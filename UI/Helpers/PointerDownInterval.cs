using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class PointerDownInterval : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float Interval = 1f;
    public UnityEvent onDown = new();

    Coroutine coInterval;


    IEnumerator Co_Interval()
    {
        while (true)
        {
            onDown.Invoke();
            yield return new WaitForSeconds(Interval);
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (coInterval != null) StopCoroutine(coInterval);
        coInterval = StartCoroutine(Co_Interval());
    }

    public void OnPointerUp(PointerEventData e)
    {
        if (coInterval != null) StopCoroutine(coInterval);
    }
}