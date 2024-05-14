using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine.EventSystems;

public class UIUtil : MonoSingleton<UIUtil>
{
    public static string AlphabetIntegerType(double d)
    {
        string s = "";
        if (d > 1000000000000)
        {
            s = (d / 1000000000000 - 0.005).ToString("F3") + "D";
        }
        if (d > 1000000000)
        {
            s = (d / 1000000000 - 0.005).ToString("F3") + "C";
        }
        else if (d > 1000000)
        {
            s = (d / 1000000 - 0.005).ToString("F3") + "B";
        }
        else if (d > 1000)
        {
            s = (d / 1000 - 0.005).ToString("F3") + "A";
        }
        else
        {
            s = d.ToString();
        }
        return s;
    }




    //차일드 전부 페이드인아웃.
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




    public static void Colorize_UIChildren(Transform parent, Color color)
    {
        Text[] txts = parent.GetComponentsInChildren<Text>();
        Image[] imgs = parent.GetComponentsInChildren<Image>();
        foreach (var item in txts)
        {
            item.color = color;
        }
        foreach (var item in imgs)
        {
            item.color = color;
        }
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



    public static string TagTMPSprite(string spr)
    {
        if (string.IsNullOrEmpty(spr))
            return "";
        else
            return string.Format("<sprite name=\"{0}\">", spr);
    }




    static string Colorize(int v, bool isPercent = false)
    {
        string operation = "";
        if (isPercent == true) operation = "%";

        if (v > 0)
            return string.Format("<color=#00ff00>{0}{1}", v, operation);
        else if (v < 0)
            return string.Format("<color=red>{0}{1}", v, operation);
        else
            return v.ToString();
    }


    // 자원.
    // 스트링 -> 메쉬 스프라이트.
    public static string ResourcesType_To_TMP(string str)
    {
        if (str == "dollar") return "$";
        if (str == "cash") return TagTMPSprite("icon_cash");
        else if (str == "coin") return TagTMPSprite("icon_coin");
        else return "";
    }

    public static Sprite GetIcon(string type)
    {
        if (type == "cash") return "Lib/empty".LoadSprite();
        return $"Icons/icon_{type}".LoadSprite();
    }
}
