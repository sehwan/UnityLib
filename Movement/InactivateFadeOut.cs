using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InactivateFadeOut : MonoBehaviour
{
    Graphic graphic;
    Color color;
    public float wait = 2;
    public float dur = 1;


    WaitForSeconds w_wait;
    WaitForSeconds w_dur;
    void Awake()
    {
        graphic = GetComponent<Graphic>();
        color = graphic.color;
        w_wait = new WaitForSeconds(wait);
        w_dur = new WaitForSeconds(dur);
    }
    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        graphic.color = color;
        yield return w_wait;
        graphic.CrossFadeAlpha(0, dur, true);
        yield return w_dur;
        gameObject.SetActive(false);
    }
}
