using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using TMPro;

public static class UiEx
{
    public static void SetActive(this Image me, bool isActive) =>
        me.gameObject.SetActive(isActive);
    public static void SetActive(this Text me, bool isActive) =>
        me.gameObject.SetActive(isActive);
    public static void SetActive(this TextMeshProUGUI me, bool isActive) =>
        me.gameObject.SetActive(isActive);
    public static void SetActive(this TMP_Text me, bool isActive) =>
        me.gameObject.SetActive(isActive);
    public static void SetActive(this Button me, bool isActive) =>
        me.gameObject.SetActive(isActive);

    public static Transform Find(this Button me, string name)
    {
        return me.transform.Find(name);
    }

    /// 텍스트 컴포넌트 간편
    public static Text GetText(this Transform me)
    {
        return me.GetComponent<Text>();
    }
    public static void Text(this Transform me, string str)
    {
        me.GetComponent<Text>().text = str;
    }
    public static void Text(this GameObject me, string str)
    {
        me.GetComponent<Text>().text = str;
    }

    public static Text GetText(this GameObject me)
    {
        return me.GetComponent<Text>();
    }
    /// 이미지 컴포넌트 간편
    public static Image GetImage(this Transform me)
    {
        return me.GetComponent<Image>();
    }
    public static Image GetImage(this GameObject me)
    {
        return me.GetComponent<Image>();
    }
    /// 스프라이트 변경 간편
    public static void SetSprite(this Image me, string path)
    {
        me.sprite = Resources.Load<Sprite>(path);
    }
    public static void SetSprite(this Image me, Sprite spr)
    {
        me.sprite = spr;
        me.SetActive(true);
    }

    public static void SetAlpha(this MaskableGraphic me, float alpha)
    {
        me.color = new Color(me.color.r, me.color.g, me.color.b, alpha);
    }

    public static string RepeatRate(this string c, float fill, float max)
    {
        var count = Mathf.RoundToInt(fill * max);
        return string.Concat(Enumerable.Repeat(c, count));
    }
    public static string RepeatRate(this char c, float fill, float max)
    {
        var count = Mathf.RoundToInt(fill * max);
        return string.Concat(Enumerable.Repeat(c, count));
    }
    public static void FillAmount(this Image me, int cur, int max)
    {
        if (max == 0) return;
        FillAmount(me, cur, (float)max);
    }
    public static void FillAmount(this Image me, long cur, long max)
    {
        if (max == 0) return;
        FillAmount(me, cur, (float)max);
    }
    public static void FillAmount(this Image me, float cur, int max)
    {
        if (max == 0) return;
        FillAmount(me, cur, (float)max);
    }
    public static void FillAmount(this Image me, float cur, float max)
    {
        if (max == 0) return;
        if (cur < 0)
        {
            me.fillAmount = 0;
            return;
        }
        if (max < 0)
        {
            me.fillAmount = 0;
            return;
        }
        me.fillAmount = cur / max;
    }

    public static void SetSpriteInNativeSize(this Image me, Sprite spr)
    {
        me.sprite = spr;
        me.rectTransform.sizeDelta = spr.bounds.size;
    }


    ///<summary>
    /// 비교해서 높으면 그린, 같으면 화이트, 낮으면 오렌지
    ///</summary>
    public static void TextCompareAndColorizedNumber(this Text me, int value, int other)
    {
        if (other < value) me.text = string.Format("<color=#00ff00>{0:n0}</color>", value);
        else if (other == value) me.text = string.Format("<color=white>{0:n0}</color>", value);
        else me.text = string.Format("<color=orange>{0:n0}</color>", value);
    }

    public static void FractionalText(this Text me, int cur, int max)
    {
        me.text = string.Format("{0:n0} / {1:n0}", cur, max);
    }
    public static void FractionalText(this Text me, long cur, long max)
    {
        me.text = string.Format("{0:n0} / {1:n0}", cur, max);
    }

    public static void Reset(this Button me, string name = null, UnityEngine.Events.UnityAction cb = null)
    {
        me.onClick.RemoveAllListeners();
        if (cb == null)
        {
            me.SetActive(false);
            return;
        }

        me.SetActive(true);
        me.onClick.AddListener(cb);
        me.transform.GetComponentInChildren<Text>().text = name.L();
    }


    public static void SetSprites(this SpriteRenderer[] me, Sprite[] sprites)
    {
        for (int i = 0; i < me.Length; i++)
        {
            if (sprites[i] == null) me[i].enabled = false;
            else
            {
                me[i].enabled = true;
                me[i].sprite = sprites[i];
            }
        }
    }
    public static void Set(this Image[] me, Sprite[] sprites)
    {
        for (int i = 0; i < me.Length; i++)
        {
            if (sprites[i] == null) me[i].SetActive(false);
            else
            {
                me[i].SetActive(true);
                me[i].sprite = sprites[i];
            }
        }
    }


    public static void DrawLevel(int lv, int exp, int req, int max, Text txt_lv, Text txt_exp, Image fill)
    {
        txt_lv.text = $"Lv.{lv:n0}";
        if (lv >= max)
        {
            txt_exp.text = "MAX";
            fill.fillAmount = 1;
        }
        else
        {
            txt_exp.text = $"{exp:n0}/{req:n0}";
            fill.fillAmount = (float)exp / req;
        }
    }
    public static void DrawLevel(int lv, long exp, long req, int max, Text txt_lv, Text txt_exp, Image fill)
    {
        txt_lv.text = $"Lv.{lv:n0}";
        if (lv >= max)
        {
            txt_exp.text = "MAX";
            fill.fillAmount = 1;
        }
        else
        {
            txt_exp.text = $"{exp:n0} / {req:n0}";
            fill.fillAmount = (float)exp / req;
        }
    }
    // the Point Can be Lowered
    public static void DrawFameLevel(int lv, long exp, long req, int max, Text txt_lv, Text txt_exp, Image fill)
    {
        txt_lv.text = $"Lv.{lv:n0}";
        if (lv >= max) txt_lv.text = "MAX";
        txt_exp.text = $"{exp:n0} / {req:n0}";
        fill.fillAmount = (float)exp / req;
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



    public static string Colorize(int v, bool isPercent = false)
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

    public static Sprite GetIcon(this string type)
    {
        if (type == "cash") return "Lib/empty".LoadSprite();
        return $"Icons/icon_{type}".LoadSprite();
    }


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
    public static string ClassifyTier(int total, int rank)
    {
        string tier = null;
        if (rank == 0)
            tier = null;
        else if (rank / (float)total > 0.75)
            tier = "icon_league_dirt_0";
        else if (rank / (float)total > 0.5)
            tier = "icon_league_bronze_0";
        else if (rank / (float)total > 0.25)
            tier = "icon_league_silver_0";
        else if (rank / (float)total > 0.1)
            tier = "icon_league_gold_0";
        else
            tier = "icon_league_diamond_0";
        return tier;
    }


    // TMP
    public static DG.Tweening.Core.TweenerCore<Color, Color, 
        DG.Tweening.Plugins.Options.ColorOptions>
        DGCrossFadeAlpha(this Graphic me, float alpha, float duration)
    {
        me.DOKill();
        return me.DOColor(new Color(me.color.r, me.color.g, me.color.b, alpha), duration);
    }

    public static string TagTMPSprite(string spr)
    {
        if (string.IsNullOrEmpty(spr))
            return "";
        else
            return string.Format("<sprite name=\"{0}\">", spr);
    }

    public static Tweener DOText(this TMP_Text me, string endValue, float duration)
    {
        var startValue = me.text;
        var progress = 0f;
        return DOTween.To(() => progress, x =>
        {
            progress = x;
            int len = Mathf.FloorToInt(Mathf.Lerp(0, endValue.Length, progress));
            me.text = endValue[..len];
        }, 1f, duration).SetEase(Ease.Linear);
    }
}