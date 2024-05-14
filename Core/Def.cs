using UnityEngine;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Def
{
    public static string[] RESERVED_NAMES =
    {
        "ADMIN", "Admin", "운영자",
        "administrator", "Administrator",
        "///", "//", "/",
        "GM", "Gm", "gm",
    };
    public const string URL_Market = "https://play.google.com/store/apps/details?id=com.alchemists.berserker";

    // System
    public static Vector2 RESOULUTION = new(1280, 720);
    public static float GetOrthographicSize() => RESOULUTION.x / 100;
    [JsonProperty] public static int AutoSaveCycle = 10;

    // Tip
    public static string RandomTip()
    {
        var length = int.Parse("tip_length".L());
        return ($"tip_{RandomEx.R(length)}").L();
    }

    // Basic
    [JsonProperty] public static int UserLevelMax = 30;
    [JsonProperty] public static int EnergyMaxDefault = 10;
    [JsonProperty] public static int EnergyCool = 600;

    // Item
    [JsonProperty] public static int EhcLimit = 10;
    public static int[] EhcChances
    {
        get
        {
            return new int[] {
            EhcChc0, EhcChc1, EhcChc2, EhcChc3, EhcChc4,
            EhcChc5, EhcChc6, EhcChc7, EhcChc8, EhcChc9,
            };
        }
    }
    [JsonProperty] public static int EhcChc0 = 100;
    [JsonProperty] public static int EhcChc1 = 90;
    [JsonProperty] public static int EhcChc2 = 80;
    [JsonProperty] public static int EhcChc3 = 70;
    [JsonProperty] public static int EhcChc4 = 60;
    [JsonProperty] public static int EhcChc5 = 50;
    [JsonProperty] public static int EhcChc6 = 40;
    [JsonProperty] public static int EhcChc7 = 30;
    [JsonProperty] public static int EhcChc8 = 20;
    [JsonProperty] public static int EhcChc9 = 10;
    public static int GetChanceToEnhance(int lv)
    {
        if (lv == 0) return 90;
        if (lv == 1) return 80;
        if (lv == 2) return 70;
        if (lv == 3) return 60;
        if (lv == 4) return 50;
        if (lv == 5) return 40;
        if (lv == 6) return 35;
        if (lv == 7) return 30;
        if (lv == 8) return 25;
        if (lv == 9) return 20;
        if (lv == 10) return 20;
        if (lv == 11) return 20;
        if (lv == 12) return 20;
        if (lv == 13) return 20;
        if (lv == 14) return 20;
        if (lv == 15) return 10;
        return 10;
    }

    // Gacha
    public static int GachaCount()
    {
        var r = Random.Range(0f, 100f);
        if (r < 1f) return 100;
        else if (r < 5f) return 10;
        else if (r < 24f) return 5;
        else return 1;
    }
    public static int NewbieProgress = 8;
    // public static int[] GradeChancesForNewbie
    // {
    //     get
    //     {
    //         var adj = NewbieProgress - User.data.stageHigh;
    //         Debug.Log($"<color=cyan>{adj}</color>");
    //         return new int[] {
    //             GradeChc0 - adj * 2,
    //             GradeChc1 - adj,
    //             GradeChc2 + adj * 2,
    //             GradeChc3 + adj };
    //     }
    // }
    public static int[] GradeChances
    { get { return new int[] { GradeChc0, GradeChc1, GradeChc2, GradeChc3 }; } }
    [JsonProperty] public static int GradeChc0 = 40;
    [JsonProperty] public static int GradeChc1 = 40;
    [JsonProperty] public static int GradeChc2 = 15;
    [JsonProperty] public static int GradeChc3 = 5;
    public static int[] GradeChancesFree
    { get { return new int[] { GradeChc0, GradeChc1, GradeChc2, GradeChc3 }; } }
    [JsonProperty] public static int GradeChcF0 = 70;
    [JsonProperty] public static int GradeChcF1 = 24;
    [JsonProperty] public static int GradeChcF2 = 5;
    [JsonProperty] public static int GradeChcF3 = 1;

    // Time
    public const int SEC_MIN = 60;
    public const int SEC_HOUR = 60 * SEC_MIN;
    public const int SEC_DAY = SEC_HOUR * 24;
    public const int SEC_WEEK = SEC_HOUR * 24;
    public const int LIMIT_AWAY_SEC = 60 * 60 * 6; // 6 hours

    //Upgradable Variables
    // public const string up_memberMax = "member";

    // public static int MEMBER_MAX
    // {
    //     get { return User.inst.GetCurrentMetaUpgrade(up_memberMax).value; }
    // }
}