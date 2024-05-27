using UnityEngine;

[System.Serializable]
public class Rsc
{
    public const string _gold = "gold";
    public const string _soul = "soul";
    public const string _gem = "gem";
    public const string _rbox = "rbox";
    public const string _ticket = "tk";
    public const string _ticketAdSkip = "tk_ad";

    public string t; // Reward Type
    public int n; // Int Number
    public BigNumber b; // Big Number
    public bool isJustShowing;

    public static Rsc New(string reward, int count, bool isJustShowing = false)
    {
        var r = new Rsc
        {
            t = reward,
            n = count,
            isJustShowing = isJustShowing
        };
        return r;
    }
    public static Rsc New(string reward, BigNumber count, bool isJustShowing = false)
    {
        var r = new Rsc
        {
            t = reward,
            b = count,
            isJustShowing = isJustShowing
        };
        return r;
    }


    public void InitToSlot(ItemSlot s)
    {
        s.SetActive(true);
        s.NormalSlotBase(null, null);
        // s.SetIcons(UIUtil.GetIcon(t));
        s.SetIcons(t.GetIcon());
        s.img_frame.color = Color.black;
        if (t == _soul) s.txt_5.text = $"{b}";
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
public class DataMail : Rsc
{
    public string ts;
}


[System.Serializable]
public class MetaAchievement : Rsc
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