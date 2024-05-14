
// 토스트 메세지.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Toast_Bak : MonoSingleton<Toast_Bak>
{
    Queue<string> _queue = new();


    [Header("Toast")]
    Image _t_image;
    Text _t_text;
    ContentSizeFitter _t_fitter;

    [Space]
    [Header("Alert")]
    Image _a_image;
    Text _a_text;
    ContentSizeFitter _a_fitter;


    new void Awake()
    {
        base.Awake();
        transform.SetParent(GameObject.Find("Canvas").transform, false);
        transform.SetAsLastSibling();
        transform.localPosition = new Vector2(0, 0);

        //Toast
        Vector2 t_pos = new(0, 0.22f);

        GameObject t_image = new("Image");
        t_image.transform.SetParent(gameObject.transform, false);
        t_image.transform.position = t_pos;
        t_image.SetActive(false);
        _t_image = t_image.AddComponent<Image>();
        _t_image.color = new Color(0.01f, 0.2f, 0.5f, 0.85f);
        _t_image.raycastTarget = false;

        // VerticalLayoutGroup t_layout = t_image.AddComponent<VerticalLayoutGroup>();
        // t_layout.padding = new RectOffset(5,5,5,5);
        // t_layout.childControlWidth = true;
        // t_layout.childControlHeight = true;
        // t_layout.childForceExpandWidth = false;
        // t_layout.childForceExpandHeight = false;

        GameObject t_text = new("Text");
        t_text.transform.SetParent(gameObject.transform, false);
        t_text.transform.position = t_pos;
        t_text.SetActive(false);
        _t_text = t_text.AddComponent<Text>();
        t_text.GetComponent<RectTransform>().sizeDelta = new Vector2(Def.RESOULUTION.x * 0.8f, 200);
        _t_text.alignment = TextAnchor.MiddleCenter;
        _t_text.raycastTarget = false;
        _t_text.fontSize = 30;
        _t_text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        // _text.font = (Font)Resources.Load("etc/NanumGothicBold");

        _t_fitter = t_text.AddComponent<ContentSizeFitter>();
        _t_fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        _t_fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        Shadow t_shadow = t_text.AddComponent<Shadow>();
        t_shadow.effectDistance = new Vector2(2, -2);

        StartCoroutine(Co_Show());


        //Alert
        Vector2 a_pos = new(0, 0.6f);

        GameObject a_image = new("Image");
        a_image.transform.SetParent(gameObject.transform, false);
        a_image.transform.position = a_pos;
        a_image.SetActive(false);
        _a_image = a_image.AddComponent<Image>();
        _a_image.color = new Color(0.01f, 0.2f, 0.5f, 0.85f);
        _a_image.raycastTarget = false;

        GameObject a_text = new("Text");
        a_text.transform.SetParent(gameObject.transform, false);
        a_text.transform.position = a_pos;
        a_text.SetActive(false);
        _a_text = a_text.AddComponent<Text>();
        a_text.GetComponent<RectTransform>().sizeDelta = new Vector2(Def.RESOULUTION.x * 0.8f, 200);
        _a_text.alignment = TextAnchor.MiddleCenter;
        _a_text.raycastTarget = false;
        _a_text.fontSize = 30;
        _a_text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        // _text.font = (Font)Resources.Load("etc/NanumGothicBold");

        _a_fitter = a_text.AddComponent<ContentSizeFitter>();
        _a_fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        _a_fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        Shadow a_shadow = a_text.AddComponent<Shadow>();
        a_shadow.effectDistance = new Vector2(2, -2);
    }


    //넣기.
    public void Enqueue(string str)
    {
        _queue.Enqueue(str);
    }
    IEnumerator Co_Show()
    {
        WaitForSeconds w = new(0.3f);
        while (true)
        {
            if (_queue.Count > 0)
            {
                string str = _queue.Dequeue();
                transform.SetAsLastSibling();
                _t_text.text = str;
                _t_text.gameObject.SetActive(true);
                _t_image.gameObject.SetActive(true);
                yield return null;

                float x = _t_text.rectTransform.sizeDelta.x;
                x = Mathf.Clamp(x, 10, Def.RESOULUTION.x * 0.8f);
                _t_fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                _t_text.rectTransform.sizeDelta = new Vector2(x, 0);

                yield return null;
                _t_image.rectTransform.sizeDelta = new Vector2(
                    _t_text.rectTransform.sizeDelta.x + 20,
                    _t_text.rectTransform.sizeDelta.y + 20);
                yield return null;

                float time = str.Length * 0.14f;
                time = Mathf.Clamp(time, 1.5f, 8f);
                yield return new WaitForSeconds(time);

                _t_fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                _t_text.gameObject.SetActive(false);
                _t_image.gameObject.SetActive(false);
            }
            yield return w;
        }
    }



    Coroutine alert;
    public void Alert(string str)
    {
        if (alert != null) StopCoroutine(alert);
        if (str.IsNullOrEmpty()) return;
        alert = StartCoroutine(Co_Alert(str));
    }
    IEnumerator Co_Alert(string str)
    {
        transform.SetAsLastSibling();
        _a_text.text = str;
        _a_text.gameObject.SetActive(true);
        _a_image.gameObject.SetActive(true);
        yield return null;

        float x = _a_text.rectTransform.sizeDelta.x;
        x = Mathf.Clamp(x, 10, Def.RESOULUTION.x * 0.8f);
        _a_fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        _a_text.rectTransform.sizeDelta = new Vector2(x, 0);

        yield return null;
        _a_image.rectTransform.sizeDelta = new Vector2(
            _a_text.rectTransform.sizeDelta.x + 20,
            _a_text.rectTransform.sizeDelta.y + 20);
        yield return null;
        float time = str.Length * 0.14f;
        time = Mathf.Clamp(time, 1.5f, 8f);
        yield return new WaitForSeconds(time);

        _a_fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        _a_text.gameObject.SetActive(false);
        _a_image.gameObject.SetActive(false);
    }
}
