using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SnowMessage : MonoBehaviour
{
    Image[] gos;
    Text[] txts;
    int idx;

    void Awake()
    {
        gos = GetComponentsInChildren<Image>();
        txts = GetComponentsInChildren<Text>();
        gos.ForEach(e => e.gameObject.SetActive(false));
    }
    public void Show(string s)
    {
        var go = gos[idx];
        go.SetActive(false);
        go.SetActive(true);
        var tr = go.transform;
        tr.SetAsLastSibling();
        tr.LocalScaleX(0f);
        tr.DOScaleX(1f, .1f);
        txts[idx].text = s;
        if (++idx == gos.Length) idx = 0;
    }
}
