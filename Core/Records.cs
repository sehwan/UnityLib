// #define STEAM

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
#if STEAM
using Steamworks;
#endif

public static class Records
{
    static bool isDirty = false;
    static Dictionary<string, object> session = new();
    static Dictionary<string, object> records = new();
    static readonly string SavePath = Path.Combine(Application.persistentDataPath, "records.sav");

#if UNITY_EDITOR
    const string ENCRYPT_KEY = "";
#else
    const string ENCRYPT_KEY = "records_key";
#endif


    public static void LoadFromFile()
    {
        if (File.Exists(SavePath) == false)
        {
            session = new Dictionary<string, object>();
            return;
        }
        var json = File.ReadAllText(SavePath).Decrypt(ENCRYPT_KEY);
        if (json.IsNullOrEmpty())
        {
            session = new Dictionary<string, object>();
            return;
        }
        session = json.ToObject<Dictionary<string, object>>();
    }

    public static void Save()
    {
        if (isDirty == false) return;
        isDirty = false;
        
#if STEAM
        SteamUserStats.StoreStats();
#else
        File.WriteAllText(SavePath, session.ToJson().Encrypt(ENCRYPT_KEY));
#endif
    }

    public static void DeleteAllRecords()
    {
#if STEAM
        SteamUserStats.ResetAll(true);
#else
        session.Clear();
#endif
        isDirty = true;
        Save();
    }

    public static void ShowRecords()
    {
        StringBuilder output = new();
        foreach (var e in session)
        {
            output.AppendLine($"{e.Key}: {e.Value}");
        }
        Debug.Log(output);
    }


    // --- Int --- 
    public static int GetInt(string key, int defaultValue = 0)
    {
#if STEAM
        return SteamUserStats.GetStatInt(key);
#else
        if (session.TryGetValue(key, out var v) && v is int i) return i;
        return defaultValue;
#endif
    }

    public static void SetInt(string key, int value)
    {
        isDirty = true;
#if STEAM
        SteamUserStats.SetStat(key, value);
#else
        session[key] = value;
#endif
    }

    public static void AddInt(string key, int amount)
    {
        SetInt(key, GetInt(key) + amount);
    }

    public static void SetIntIfHigher(string key, int value)
    {
        if (value <= GetInt(key)) return;
        SetInt(key, value);
    }


    // --- Float ---
    public static float GetFloat(string key, float defaultValue = 0f)
    {
#if STEAM
        return SteamUserStats.GetStatFloat(key);
#else
        if (session.TryGetValue(key, out var v) && v is float f) return f;
        return defaultValue;
#endif
    }

    public static void SetFloat(string key, float value)
    {
        isDirty = true;
#if STEAM
        SteamUserStats.SetStat(key, value);
#else
        session[key] = value;
#endif
    }

    public static void AddFloat(string key, float amount)
    {
        SetFloat(key, GetFloat(key) + amount);
    }

    public static void SetFloatIfHigher(string key, float value)
    {
        if (value <= GetFloat(key)) return;
        SetFloat(key, value);
    }


    // --- Achievements ---
    public static bool GetAchievement(string key)
    {
#if STEAM
        bool achieved = false;
        return achieved;
#else
        return GetInt(key, 0) == 1;
#endif
    }

    public static void SetAchievement(string key)
    {
#if STEAM
        if (GetAchievement(key)) return;
        SteamUserStats.Achievements.SetField(key, true);
        SteamUserStats.StoreStats();
#else
        SetInt(key, 1);
#endif
    }
}