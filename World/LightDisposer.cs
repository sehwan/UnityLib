using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightDisposer : MonoBehaviour
{
    [Header("in the Circle?")]
    public float radius;

    [Header("in the Rect?")]
    public float w = 40;
    public float h = 70;

    [Header("Size")]
    public float scaleMin, scaleMax;


    public void Randomize(int chance)
    {
        foreach (Transform c in transform)
        {
            var isOn = chance.Percent();
            c.SetActive(isOn);
            if (isOn == false) continue;

            // pos
            if (radius != 0)
                c.localPosition = VectorEx.Random2(radius);
            else if (w != 0 || h != 0)
                c.localPosition = new Vector2(RandomEx.R(w, -w), RandomEx.R(h, -h));

            // scale
            c.localScale = new Vector2(RandomEx.R(scaleMax, scaleMin), RandomEx.R(scaleMax, scaleMin));
        }
    }


    public void HideAll()
    {
        foreach (Transform c in transform) c.SetActive(false);
    }
}
