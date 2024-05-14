// Using for Server

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Resource
{
    public const string kGold = "gold";
    public const string kSoul = "soul";
    public const string kGem = "gem";
    public const string kRbox = "rbox";
    public const string kTicket = "ticket";
    public const string kTicketAdSkip = "tk_ad";

    public string r; // Reward Type
    public int n; // Int Number
    public BigNumber b; // Big Number
    public bool isJustShowing;

    #region Factory
    public static Resource Gold(int count, bool isJustShowing = false)
    {
        var r = new Resource();
        r.r = kGold;
        r.n = count;
        r.isJustShowing = isJustShowing;
        return r;
    }
    public static Resource Soul(BigNumber count, bool isJustShowing = false)
    {
        var r = new Resource();
        r.r = kSoul;
        r.b = count;
        r.isJustShowing = isJustShowing;
        return r;
    }
    public static Resource Gem(int count, bool isJustShowing = false)
    {
        var r = new Resource();
        r.r = kGem;
        r.n = count;
        r.isJustShowing = isJustShowing;
        return r;
    }
    public static Resource Rbox(int count, bool isJustShowing = false)
    {
        var r = new Resource();
        r.r = kRbox;
        r.n = count;
        r.isJustShowing = isJustShowing;
        return r;
    }
    public static Resource Ticket(int count, bool isJustShowing = false)
    {
        var r = new Resource();
        r.r = kTicket;
        r.n = count;
        r.isJustShowing = isJustShowing;
        return r;
    }
    public static Resource TicketAd(int count, bool isJustShowing = false)
    {
        var r = new Resource();
        r.r = kTicketAdSkip;
        r.n = count;
        r.isJustShowing = isJustShowing;
        return r;
    }
    #endregion

    public static Resource New(string reward, int count, bool isJustShowing = false)
    {
        var r = new Resource();
        r.r = reward;
        r.n = count;
        r.isJustShowing = isJustShowing;
        return r;
    }
    public static Resource New(string reward, BigNumber count, bool isJustShowing = false)
    {
        var r = new Resource();
        r.r = reward;
        r.b = count;
        r.isJustShowing = isJustShowing;
        return r;
    }


    public void InitToSlot(ItemSlot s)
    {
        s.SetActive(true);
        s.NormalSlotBase(null, null);
        s.SetIcons(UIUtil.GetIcon(r));
        s.img_frame.color = Color.black;
        if (r == kSoul) s.txt_5.text = $"{b}";
        else s.txt_5.text = $"{n:n0}";
    }

    public bool IsPayable()
    {
        return true;
        // if (r == kGem) return (User.data.gem + n) >= 0;
        // else return false;
    }
}



[System.Serializable]
public class DataMail : Resource
{
    public string t;
}


[System.Serializable]
public class MetaAchievement : Resource
{
    public string id;
    public string type;
    public int lv;
    public int req;

    // public static MetaAchievement GetValue(string type, int level)
    // {
    //     List<MetaAchievement> tables = GameData.meta.achv[type];
    //     if (level >= tables.Count) return null;
    //     else return tables[level];
    // }
}