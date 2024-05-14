using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class QuestsPanel : UIWindow
{
    public GameObject prefab;
    public Transform tr_daily, tr_repeating;
    public Dictionary<string, QuestItem> daily = new();
    public Dictionary<string, QuestRepeatingItem> repeating = new();


    public override void Init()
    {
        base.Init();

        tr_daily.Pool(prefab, GameData.meta.dq.Count);
        for (int i = 0; i < GameData.meta.dq.Count; i++)
        {
            var meta = GameData.meta.dq.ElementAt(i);
            daily.Add(meta.Key, tr_daily.GetChild(i).GetComponent<QuestItem>());
            daily[meta.Key].Init(meta.Value);
        }

        tr_repeating.Pool(prefab, GameData.meta.rq.Count);
        for (int i = 0; i < GameData.meta.rq.Count; i++)
        {
            var meta = GameData.meta.rq.ElementAt(i);
            repeating.Add(meta.Key, tr_repeating.GetChild(i).GetComponent<QuestRepeatingItem>());
            repeating[meta.Key].Init(meta.Value);
        }
        CheckNoti();
    }

    public override void Show()
    {
        base.Show();
        foreach (var e in daily.Values)
        {
            e.Refresh();
            if (e.isAble) e.transform.SetAsFirstSibling();
            if (e.go_completed.activeSelf) e.transform.SetAsLastSibling();
        }
        foreach (var e in repeating.Values)
        {
            e.Refresh();
            if (e.isAble) e.transform.SetAsFirstSibling();
        }
        var d = daily.Values.Any(e => e.isAble);
        var r = repeating.Values.Any(e => e.isAble);
        NotiBadges.Noti("daily", d);
        NotiBadges.Noti("repeating", r);
    }

    public void CheckNoti()
    {
        var d = daily.Values.Any(e => e.isAble);
        var r = repeating.Values.Any(e => e.isAble);
        NotiBadges.Noti("daily", d);
        NotiBadges.Noti("repeating", r);
        NotiBadges.Noti("quest", d || r);
    }
}