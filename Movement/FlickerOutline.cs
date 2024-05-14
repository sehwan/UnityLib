using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlickerOutline : MonoBehaviour
{
    public float PERIOD = 0.6f;
    public float MAX = 1;
    public float MIN = 0.4f;
    public float DELAY = 0;


    void OnEnable()
    {
        StartCoroutine(Co_Flickering());
    }

    IEnumerator Co_Flickering()
    {
        var target = GetComponent<Outline>();
        var originColor = target.effectColor;
        var colorMax = originColor.SetAlpha(MAX);
        var colorMin = originColor.SetAlpha(MIN);
        yield return new WaitForSecondsRealtime(DELAY);
        WaitForSecondsRealtime w = new(PERIOD);
        while (true)
        {
            target.DOColor(colorMax, PERIOD).SetUpdate(true);
            yield return w;
            target.DOColor(colorMin, PERIOD).SetUpdate(true);
            yield return w;
        }
    }
}
