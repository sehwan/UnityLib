using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioText : MonoBehaviour
{
    public Text txt_text;
    public RectTransform rect_text;
    float width;

    void Awake()
    {
        width = rect_text.sizeDelta.x;
        txt_text.text = "";
        Show("리빙에센스! 돈이 부족할 땐 알바를 하면 좋다.");
    }
    public void Show(string text)
    {
        txt_text.text = text;
        co_scroll = StartCoroutine(Co_Scroll());
    }
    Coroutine co_scroll;
    IEnumerator Co_Scroll()
    {
        // if (txt_text.rectTransform.sizeDelta.x <= width) yield break;
        yield return CoroutineEx.GetWait(1);
        while (true)
        {
            yield return null;
            rect_text.Translate(0.3f * Time.deltaTime * Vector2.left);
            if (txt_text.transform.localPosition.x < -width - txt_text.rectTransform.sizeDelta.x)
            {
                txt_text.transform.localPosition = new Vector2(width, 0);
                yield return CoroutineEx.GetWait(1);
            }
        }
    }
}
