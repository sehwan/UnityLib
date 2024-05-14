using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MetaData
{

    public Dictionary<int, Resource> att = new();
    public Dictionary<int, Resource> online = new();

    public Dictionary<string, QuestMeta> dq = new();
    public Dictionary<string, QuestMeta> rq = new();
    // public Dictionary<string, MetaAchievement> raw_achv = new Dictionary<string, MetaAchievement>();
    // public Dictionary<string, List<MetaAchievement>> achv = new Dictionary<string, List<MetaAchievement>>();

    // public Dictionary<string, DataReward> coupons = new Dictionary<string, DataReward>();
    // public DataRank myRank = new DataRank();

    // public Dictionary<string, IAPMeta> iap = new Dictionary<string, IAPMeta>();

    public void OnInit()
    {
        // Achievements
        // foreach (var e in raw_achv.Values)
        // {
        //     achv.GetValueDefault(e.type).Add(e);
        // }
        
        // dqArr = dq.Values.ToArray();
        // rqArr = rq.Values.ToArray();
    }
}