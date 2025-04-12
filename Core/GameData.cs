using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
// using Firebase.RemoteConfig;

public class GameData : MonoSingleton<GameData>
{
    [Header("Mode")]
    [Immutable] public bool isServer = true;
    [Immutable] public TextAsset textAsset;

    [Header("Data")]
    public MetaData _meta = new();
    public static MetaData meta { get { return i._meta; } }
    [Immutable] public bool didInitMeta;

    [Header("Dynamic")]
    public int census = 1;
    // public RankData[] rankers_pvp = new RankData[0];




    public /* async */ void Start()
    {
        // Client?
        if (isServer == false)
        {
            InitDataFromFile();
            return;
        }

        // From Server
        // Meta
        // while (FirebaseMng.inst.didFetchConfig == false) await Task.Delay(50);
        // var config = FirebaseRemoteConfig.DefaultInstance;
        // var json = config.GetValue($"v{Application.version}").StringValue;
        // InitMeta(json);

        // Override Defines
        // var virtualDef = JsonConvert.DeserializeObject<Def>(config.GetValue("define").StringValue); // Essential
        // var virtualDef = JsonConvert.DeserializeObject<Def>(JObject.Parse(json)["def"].ToString());

        FetchRunningMeta();

        // var coupons = FirebaseRemoteConfig.GetValue("coupons").StringValue;
        // _meta.coupons = JsonConvert.DeserializeObject<Dictionary<string, DataReward>>(coupons);
    }


    public bool didFetchedRunningMeta;
    public void FetchRunningMeta(Action cb = null)
    {
        // FirebaseMng.inst.GetDB("meta", "meta", r =>
        // {
        //     didFetchedRunningMeta = true;
        //     var dic = r.ToDictionary();
        //     if (r.ToString().IsNullOrEmpty()) return;
        //     if (dic.ContainsKey("leaderboard"))
        //     {
        //         // Stage
        //         var json = dic["leaderboard"].ToJson();
        //         rankers_stage = json.ToObject<RankData[]>();
        //         PlayerPrefs.SetString("rankers", json);
        //         // PVP
        //         json = dic["ranks_mmr"].ToJson();
        //         rankers_pvp = json.ToObject<RankData[]>();
        //         cb?.Invoke();
        //     }
        // });
    }
    public void FetchAfterUser()
    {
        // var fb = FirebaseMng.inst;
        // var id = fb.user.UserId;
        // fb.GetRTDB($"ranks/{id}", (r) =>
        // {
        //     if (r.IsNullOrEmpty()) return;
        //     User.data.rank = int.Parse(r) + 1;
        // });
        // fb.GetRTDB($"ranks_mmr/{id}", (r) =>
        // {
        //     if (r.IsNullOrEmpty()) return;
        //     User.data.rank_pvp = int.Parse(r) + 1;
        // });
    }



    void InitDataFromFile()
    {
        InitMeta(textAsset.text);
    }

    void InitMeta(string s)
    {
        // DebugConfig(s);
        _meta = JsonConvert.DeserializeObject<MetaData>(s);
        _meta.OnInit();
        didInitMeta = true;
    }
    void DebugConfig(string s)
    {
        var splitted = s.SplitByLength(8192);
        foreach (var item in splitted)
            Debug.Log($"<color=cyan>{item}</color>");
    }
}
