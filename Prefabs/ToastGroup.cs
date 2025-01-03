using UnityEngine;
using UnityEngine.UI;

public class ToastGroup : MonoBehaviour
{
    static ToastGroup _i = null;
    public static ToastGroup inst
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameObject>("Prefabs/ToastGroup")).GetComponent<ToastGroup>();
                _i.gos = _i.GetComponentsInChildren<Image>(true);
                _i.txts = _i.GetComponentsInChildren<Text>(true);
                _i.gos.ForEach(e => e.SetActive(false));
            }
            return _i;
        }
    }

    Image[] gos;
    Text[] txts;
    public static int idx;

    public Color color_img = ColorEx.darker;
    public Color color_txt = Color.white;
    public Color color_alert_img = ColorEx.darker;
    public Color color_alert_txt = Color.yellow;

    [ContextMenu("TestColors")]
    public void TestColors()
    {
        var gos = GetComponentsInChildren<Image>(true);
        var txts = GetComponentsInChildren<Text>(true);
        for (int i = 0; i < gos.Length; i++)
        {
            if (50.Percent())
            {
                gos[i].color = color_img;
                txts[i].color = color_txt;
            }
            else
            {
                gos[i].color = color_alert_img;
                txts[i].color = color_alert_txt;
            }
        }
    }

    public static void Show(string s)
    {
        Debug.Log($"-- {s}");
        var _ = inst;
        var go = _.gos[idx];
        go.SetActive(true);
        go.transform.SetAsLastSibling();
        go.color = _.color_img;
        // tr.LocalScaleY(0f);
        // tr.DOScaleY(1f, .1f).SetUpdate(true);
        var txt = _.txts[idx];
        txt.text = s;
        txt.color = _.color_txt;

        idx++;
        if (idx == _.gos.Length) idx = 0;

        SFX.Play("beep");
        go.StopAllCoroutines();
    }
    public static void Alert(string s)
    {
        Debug.Log($"!-- {s}");
        var _ = inst;
        var go = _.gos[idx];
        go.SetActive(true);
        go.transform.SetAsLastSibling();
        go.color = _.color_alert_img;
        // tr.LocalScaleY(0f);
        // tr.DOScaleY(1f, .1f).SetUpdate(true);
        var txt = _.txts[idx];
        txt.text = s;
        txt.color = _.color_alert_txt;

        idx++;
        if (idx == _.gos.Length) idx = 0;

        SFX.Play("beep");
        go.StopAllCoroutines();
    }
}
