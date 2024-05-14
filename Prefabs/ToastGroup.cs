using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToastGroup : MonoBehaviour
{
    static ToastGroup _inst = null;
    public static ToastGroup inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = Instantiate(Resources.Load<GameObject>("Prefabs/ToastGroup")).GetComponent<ToastGroup>();
                _inst.gos.ForEach(e => e.SetActive(false));
            }
            return _inst;
        }
    }

    public Image[] gos;
    public Text[] txts;
    public static int idx;
    public Color color_txt = Color.white;
    public Color color_alert_txt = new(1, 0.76f, 0.2f);
    public float HidingTime = 1.5f;
    public float HidingTimeAlert = 1.7f;

    public static void Show(string s)
    {
        Debug.Log($"-- {s}");
        var _ = inst;
        var go = _.gos[idx];
        go.SetActive(true);
        var tr = go.transform;
        tr.SetAsLastSibling();
        tr.LocalScaleY(0f);
        tr.DOScaleY(1f, .1f).SetUpdate(true);
        _.txts[idx].text = s;
        go.color = _.color_txt;
        idx++;
        if (idx == _.gos.Length) idx = 0;

        SFX.Play("beep");
        go.StopAllCoroutines();
        go.StartCoroutine(_.Co_Hide(go, _.HidingTime));
    }
    public static void Alert(string s)
    {
        Debug.Log($"!-- {s}");
        var _ = inst;
        var go = _.gos[idx];
        go.SetActive(true);
        var tr = go.transform;
        tr.SetAsLastSibling();
        tr.LocalScaleY(0f);
        tr.DOScaleY(1f, .1f).SetUpdate(true);
        _.txts[idx].text = s;
        go.color = _.color_alert_txt;
        idx++;
        if (idx == _.gos.Length) idx = 0;

        SFX.Play("beep");
        go.StopAllCoroutines();
        go.StartCoroutine(_.Co_Hide(go, _.HidingTimeAlert));
    }

    IEnumerator Co_Hide(Image go, float time)
    {
        yield return CoroutineEx.GetReal(time);
        go.SetActive(false);
    }
}
