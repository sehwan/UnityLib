using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIUtil : MonoSingleton<UIUtil>
{
    // 차일드 전부 페이드인아웃.
    public IEnumerator Co_FadeAllChild(Transform parent, float on, float delay, float off, Action cb)
    {
        parent.gameObject.SetActive(true);
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
            if (child.Find("Text"))
                child.Find("Text").GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
            if (child.Find("Image"))
                child.Find("Image").GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        }

        foreach (Transform child in parent)
        {
            if (child.Find("Text"))
                child.Find("Text").GetComponent<Text>().CrossFadeAlpha(1, on, false);
            if (child.Find("Image"))
                child.Find("Image").GetComponent<Image>().CrossFadeAlpha(1, on, false);
            yield return new WaitForSeconds(on + delay);
            if (child.Find("Text"))
                child.Find("Text").GetComponent<Text>().CrossFadeAlpha(0, off, false);
            if (child.Find("Image"))
                child.Find("Image").GetComponent<Image>().CrossFadeAlpha(0, off, false);
            yield return new WaitForSeconds(off);
            child.gameObject.SetActive(false);
        }
        cb();
    }

    public IEnumerator Co_Prologue(List<string> texts, float on, float delay, float off, Action cb)
    {
        GameObject go = new("prologue");
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.one;
        go.transform.SetAsLastSibling();
        go.AddComponent<RectTransform>().sizeDelta = new Vector2(Def.RESOULUTION.x * 0.85f, 80);

        Text text = go.AddComponent<Text>();
        text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text.fontSize = (int)Def.RESOULUTION.x / 20;
        text.lineSpacing = 1.8f;
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.verticalOverflow = VerticalWrapMode.Overflow;

        foreach (string str in texts)
        {
            text.text = str;
            text.CrossFadeAlpha(1, on, false);

            yield return new WaitForSeconds(on + delay);

            text.CrossFadeAlpha(0, off, false);

            yield return new WaitForSeconds(off);
        }
        Destroy(go);
        cb();
    }


    public float deltaClick;
    int clickCount;
    ///<summary>
    /// 그냥 클릭인지 더블클릭인지? x => if (t == 1) or 2
    ///</summary>
    public void CheckClickOrDoubleClick(Action<int> cb)
    {
        clickCount++;
        deltaClick = Time.time;
        StartCoroutine(Co_Click(cb));
    }
    IEnumerator Co_Click(Action<int> cb)
    {
        yield return new WaitForSeconds(0.3f);
        if (clickCount == 1) cb(1);
        else if (clickCount > 1) cb(2);
        clickCount = 0;
    }
}
