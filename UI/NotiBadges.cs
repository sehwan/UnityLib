using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NotiBadges : MonoBehaviour
{
    // Static
    public static Dictionary<string, NotiBadges> dic = new();
    public static void Noti(string name, int amount)
    {
        if (dic.ContainsKey(name) == false)
        {
            Debug.LogError($"<color=cyan>No Badge Object of '{name}'</color>");
            return;
        }
        var badge = dic[name];
        if (amount == 0) badge.gameObject.SetActive(false);
        else
        {
            badge.gameObject.SetActive(true);
            if (amount > 99) badge.txt_cnt.text = "!!";
            else badge.txt_cnt.text = amount.ToString();
        }
    }
    public static void Noti(string name, bool b)
    {
        if (dic.ContainsKey(name) == false)
        {
            Debug.Log($"<color=yellow>No Badge Object of '{name}'</color>");
            return;
        }

        var badge = dic[name];
        badge.gameObject.SetActive(b);
        if (badge.txt_cnt != null) badge.txt_cnt.text = "!";
    }

    // Instance
    public Text txt_cnt;
    Vector2 oriPos;
    void Awake()
    {
        dic.Add(transform.name, this);
        gameObject.SetActive(false);
        oriPos = transform.localPosition;
    }
    const string _Jump = "Jump";
    void OnEnable()
    {
        InvokeRepeating(_Jump, RandomEx.R(2f, 1f), 3f);
    }
    void Jump()
    {
        if (gameObject.activeSelf == false) return;
        transform.DOLocalJump(oriPos, 6, 1, 0.2f);
    }
}
