using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlickerUIErratically : MonoBehaviour
{
    public float alphaMax = 1;
    public float alphaMin = 0.5f;
    public float on = 2.3f;
    public float trans = 0.1f;
    public bool isPlayingInPause = false;


    void OnEnable()
    {
        StartCoroutine(Co());
    }
    IEnumerator Co()
    {
        var ui = GetComponent<MaskableGraphic>();
        while (true)
        {
            ui.CrossFadeAlpha(alphaMax, RandomEx.R(trans), true);
            if (isPlayingInPause)
                yield return new WaitForSecondsRealtime(RandomEx.R(on));
            else yield return new WaitForSeconds(RandomEx.R(on));
            var t = RandomEx.R(trans);
            ui.CrossFadeAlpha(RandomEx.R(alphaMax, alphaMin), t, true);
            if (isPlayingInPause)
                yield return new WaitForSecondsRealtime(t);
            else yield return new WaitForSeconds(t);
        }
    }
}
