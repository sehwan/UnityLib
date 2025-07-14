// #define STEAM

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using Newtonsoft.Json;
#if STEAM
using Steamworks;
#endif

public class Record
{
    public Type type;
    public object persist; // For persistent storage
    public object session; // For current session
    public bool ifHigh;
    public bool isSteam;

    public Record(Type type, bool ifHigh, bool isSteam)
    {
        this.type = type;
        this.ifHigh = ifHigh;
        this.isSteam = isSteam;
        persist = Activator.CreateInstance(type);
        session = Activator.CreateInstance(type);
    }
}

public class Records
{
    static bool isDirty = false;
    static Dictionary<string, Record> records = new();
    static readonly string SavePath = Path.Combine(Application.persistentDataPath, "records.sav");

    public static void ModelCum<T>(string key, bool isSteam = true)
    {
        if (!records.ContainsKey(key))
            records[key] = new Record(typeof(T), false, isSteam);
        else
        {
            var rec = records[key];
            rec.ifHigh = false;
            rec.isSteam = isSteam;
        }
    }

    public static void ModelHi<T>(string key, bool isSteam = true)
    {
        if (!records.ContainsKey(key))
            records[key] = new Record(typeof(T), true, isSteam);
        else
        {
            var rec = records[key];
            rec.ifHigh = true;
            rec.isSteam = isSteam;
        }
    }


    public static void Accumulate<T>(string key, T value) where T : struct, IComparable
    {
        if (!records.TryGetValue(key, out var e))
        {
            Debug.LogError($"Record with key '{key}' does not exist. Please create it first.");
            return;
        }
        if (e.ifHigh)
        {
            Debug.LogError($"Cannot accumulate on a high record '{key}'. Use Apply instead.");
            return;
        }
        e.session = Add(e.session, value);
        e.persist = Add(e.persist, value);
        Apply<T>(key, e);
    }
    public static void UpdateIfHi<T>(string key, T value) where T : struct, IComparable
    {
        if (!records.TryGetValue(key, out var e))
        {
            Debug.LogError($"Record with key '{key}' does not exist. Please create it first.");
            return;
        }
        if (!e.ifHigh)
        {
            Debug.LogError($"Cannot update high record '{key}'. Use Accumulate instead.");
            return;
        }
        if (((IComparable)e.session).CompareTo(value) < 0) e.session = value;
        if (((IComparable)e.persist).CompareTo(value) < 0) e.persist = value;
        Apply<T>(key, e);
    }

    static void Apply<T>(string key, Record e) where T : struct
    {
#if STEAM
        if (e.isSteam == false) return;
        if (e.type == typeof(long))
            Steamworks.SteamUserStats.SetStat(key, Convert.ToInt64(e.persist));
        else if (e.type == typeof(int))
            Steamworks.SteamUserStats.SetStat(key, Convert.ToInt32(e.persist));
        else if (e.type == typeof(float) || e.type == typeof(double))
            Steamworks.SteamUserStats.SetStat(key, Convert.ToSingle(e.persist));
#endif
        isDirty = true;
    }

    static object Add(object a, object b)
    {
        if (a is int ai && b is int bi) return ai + bi;
        if (a is float af && b is float bf) return af + bf;
        if (a is long al && b is long bl) return al + bl;
        if (a is double ad && b is double bd) return ad + bd;
        throw new InvalidOperationException($"Unsupported type for Add: {a?.GetType()} {b?.GetType()}");
    }


    // Current Session
    public static object GetSession(string key)
    {
        if (records.TryGetValue(key, out var rec)) return rec.session;
        Debug.LogWarning($"Record '{key}' does Not Exist.");
        return null;
    }

    public static object GetPersistent(string key)
    {
        if (records.TryGetValue(key, out var rec)) return rec.persist;
        Debug.LogWarning($"Record '{key}' does Not Exist.");
        return null;
    }



    public static void LoadFromFile()
    {
        if (File.Exists(SavePath) == false)
        {
            records = new Dictionary<string, Record>();
            return;
        }
        var json = File.ReadAllText(SavePath);
#if UNITY_EDITOR
        Debug.Log($"records: {json}");
#endif
        try
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            records = JsonConvert.DeserializeObject<Dictionary<string, Record>>(json, settings);
            isDirty = false;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load records from file: {e.Message}");
            records = new Dictionary<string, Record>();
        }
    }



    public static void ClearSession()
    {
        foreach (var rec in records.Values)
            rec.session = Activator.CreateInstance(rec.type);
        isDirty = true;
        Save();
    }

    public static void Save()
    {
        if (isDirty == false) return;
        isDirty = false;
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var json = JsonConvert.SerializeObject(records, settings);
        Debug.Log($"<color=green>{json}</color>");
        try
        {
            File.WriteAllText(SavePath, json);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save records to file: {e.Message}");
        }
    }
    public static void Reset()
    {
        records.Clear();
        isDirty = true;
        Save();
    }
    public static void ShowAll()
    {
        StringBuilder sb = new();
        foreach (var kv in records)
            sb.AppendLine($"{kv.Key}: {kv.Value.type} {(kv.Value.ifHigh ? "High" : "Cumulative")} {kv.Value.session} / {kv.Value.persist} ");
        Debug.Log(sb.ToString());
    }
}