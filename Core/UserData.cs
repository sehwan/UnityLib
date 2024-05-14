using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class UserData
{
    [Header("Base")]
    public string id;
    public string nick;
    public string device;
    public bool active;
    public string black;
    public bool tester;

    [Header("In Game")]
    public int lv = 1;

    [Space]
    [Header("Main")]

    [Space]
    [Header("Resources")]
    public int gem = 700;
    public BigNumber soul = 10_000;
    public int gold = 100;
    public int rbox = 12;
    public int exEnergy;
    public DateTime dt_energy; // not used
    public int ticket = 5;

    [Space]
    [Header("Stage")]
    public int stageHigh;

    [Space]
    [Header("Time")]
    public int att = 0;
    public bool attCheck;
    public ulong lastLogin;
    public DateTime dt_saved;
    public DateTime dt_ads;

    [Space]
    [Header("ETC")]
    public List<string> coupons = new();
    public List<string> rcd_str = new();
    public Dictionary<string, int> rcd_int = new();
    public Dictionary<string, DateTime> dts_iap = new();
    public List<int> rcd_gacha = new();
    public float gachaRate;
    public int vip_exp;

    [Space]
    [Header("Records")]
    // public Dictionary<string, PassData> pass = new Dictionary<string, PassData>();
    public int tut;
    public bool didFillRecommender;
    public int starRating;
    public int time;
    public int time_m;
    public int onlineCheck;
    public Dictionary<string, QuestData> dq = new();
    public Dictionary<string, QuestData> rq = new();

    static public string DefaultNick
    {
        get
        {
            // if (Application.isMobilePlatform && Social.localUser.userName.IsFilled())
            //     return Social.localUser.userName;
            // if (FirebaseMng.inst.user != null) return FirebaseMng.inst.user.UserId;
            return SystemInfo.deviceUniqueIdentifier;
        }
    }

    static public UserData Default()
    {
        Debug.Log("<color=cyan>Default User</color>");
        var r = new UserData();
        // r.id = FirebaseMng.inst.user.UserId;
        r.nick = DefaultNick;
        return r;
    }
    static public UserData Tester()
    {
        Debug.Log("<color=cyan>Tester User</color>");
        var r = new UserData();
        // r.id = FirebaseMng.inst.user.UserId;
        r.nick = DefaultNick;
        r.tester = true;
        r.tut = -1;
        return r;
    }

    public bool IsTutorialOver() => tut == -1;
    // public bool IsNewbie() => Progress <= Def.NewbieProgress;
    public bool IsFilled() => nick.IsNullOrEmpty() == false;


    public int GetEnergyCount() => dt_energy.GetChance(600, Def.EnergyMaxDefault + exEnergy);

    public void ConsumeEnergy()
    {
        if (exEnergy > 0) exEnergy--;
        else dt_energy = dt_energy.GetTimeUsedChance(Def.EnergyCool, Def.EnergyMaxDefault + exEnergy);
    }

    // public bool UseTicket(StageMode mode, int req = 1)
    // {
    //     // if (stage[(int)mode] < req) return false;
    //     if (ticket < req)
    //     {
    //         ToastGroup.Show("No Ticket".L());
    //         return false;
    //     }
    //     ticket -= req;
    //     User.inst.ReportAccrued("ticket", req);
    //     return true;
    // }
}


public class DataLogin
{
    public UserData user;
    public ulong now;
}