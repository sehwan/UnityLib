using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class FirebaseChatData
{
    // Colors
    const string COLOR_ADMIN = "orange";
    const string COLOR_SPECIAL = "yellow";
    const string COLOR_MINE = "lime";
    const string COLOR_OTHER = "white";

    // Message Types
    public const string TYPE_SHOUT = "shout";

    // Field
    public string n; // Nick
    public string m; // Message
    public string t; // Type
    public string h; // Helmet

    public FirebaseChatData(string nick, string type, string msg)
    {
        this.n = nick;
        this.t = type;
        this.m = msg;
    }

    public string To1LineString()
    {
        return $"{n} : ".TagColor(GetNickColor(n)) + $"{GetFinalMessage()}";
    }
    public string GetFinalMessage()
    {
        var r = m;
        // if (t == "sstone") r = $"[{m.ToObject<DataSStone>().GetNameLong()}]";
        // else if (t == "tbuff") r = $"[{m.ToObject<DataTBuff>().GetNameLong()}]";
        return r.TagColor(GetMessageColor(n, t));
    }

    bool IsMineMessage => n == GetNick();
    string GetNickColor(string nick)
    {
        if (nick == "Admin") return COLOR_ADMIN;
        return COLOR_OTHER;
    }
    string GetMessageColor(string nick, string type)
    {
        // Admin
        if (nick == "Admin") return COLOR_ADMIN;
        // Special Message
        else if (type.IsFilled()) return COLOR_SPECIAL;
        // Mine?
        if (IsMineMessage) return COLOR_MINE;
        // Other
        return COLOR_OTHER;
    }
    public static string GetNick() => User.data.nick;
}