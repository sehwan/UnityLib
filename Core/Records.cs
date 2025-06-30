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

public class Records
{
    public object persist; // For persistent storage
    public object session; // For current session
    public Type type;
    public bool ifHigh;
    public bool isSteam;

    public Records(Type type, bool ifHigh, bool isSteam)
    {
        this.type = type;
        this.ifHigh = ifHigh;
        this.isSteam = isSteam;
        persist = null;
        session = null;
    }


    static bool isDirty = false;
    static Dictionary<string, Records> records = new();
    static readonly string SavePath = Path.Combine(UnityEngine.Application.persistentDataPath, "records.sav");

    public static void Model(string key, Type type, bool isHigh, bool isSteam = false)
    {
        if (records.ContainsKey(key) == false)
            records[key] = new Records(type, isHigh, isSteam);
        else
        {
            var rec = records[key];
            rec.type = type;
            rec.ifHigh = isHigh;
            rec.isSteam = isSteam;
        }
    }
    public static void Operate(string key, object value)
    {
        if (records.TryGetValue(key, out var rec) == false)
        {
            Debug.LogError($"Record with key '{key}' does not exist. Please create it first.");
            return;
        }
        if (rec.ifHigh)
        {
            if (rec.persist == null) rec.persist = value;
            else if (rec.type == typeof(int)) rec.persist = Math.Max(Convert.ToInt32(rec.persist), Convert.ToInt32(value));
            else if (rec.type == typeof(long)) rec.persist = Math.Max(Convert.ToInt64(rec.persist), Convert.ToInt64(value));
            else if (rec.type == typeof(float)) rec.persist = Math.Max(Convert.ToSingle(rec.persist), Convert.ToSingle(value));
            else if (rec.type == typeof(double)) rec.persist = Math.Max(Convert.ToDouble(rec.persist), Convert.ToDouble(value));
        }
        else
        {
            if (rec.persist == null)
            {
                rec.session = value;
                rec.persist = value;
            }
            else if (rec.type == typeof(int))
            {
                rec.session = Convert.ToInt32(rec.session) + Convert.ToInt32(value);
                rec.persist = Convert.ToInt32(rec.persist) + Convert.ToInt32(value);
            }
            else if (rec.type == typeof(long))
            {
                rec.session = Convert.ToInt64(rec.session) + Convert.ToInt64(value);
                rec.persist = Convert.ToInt64(rec.persist) + Convert.ToInt64(value);
            }
            else if (rec.type == typeof(float))
            {
                rec.session = Convert.ToSingle(rec.session) + Convert.ToSingle(value);
                rec.persist = Convert.ToSingle(rec.persist) + Convert.ToSingle(value);
            }
            else if (rec.type == typeof(double))
            {
                rec.session = Convert.ToDouble(rec.session) + Convert.ToDouble(value);
                rec.persist = Convert.ToDouble(rec.persist) + Convert.ToDouble(value);
            }
        }
// #if STEAM
        if (rec.isSteam == false || key == null) return;
        if (rec.type == typeof(long))
            Steamworks.SteamUserStats.SetStat(key, Convert.ToInt64(rec.persist));
        else if (rec.type == typeof(int))
            Steamworks.SteamUserStats.SetStat(key, Convert.ToInt32(rec.persist));
        else if (rec.type == typeof(float) || rec.type == typeof(double) )
            Steamworks.SteamUserStats.SetStat(key, Convert.ToSingle(rec.persist));
// #endif
        isDirty = true;
    }
    public static object GetHigh(string key)
    {
        if (records.TryGetValue(key, out var rec) && rec.ifHigh)
            return rec.persist;
        Debug.LogWarning($"Record with key '{key}' is not a high record or does not exist.");
        return null;
    }
    public static object GetSession(string key)
    {
        if (records.TryGetValue(key, out var rec) && rec.ifHigh == false)
            return rec.session;
        Debug.LogWarning($"Record with key '{key}' is not a session record or does not exist.");
        return null;
    }
    public static object GetPersist(string key)
    {
        if (records.TryGetValue(key, out var rec) && rec.ifHigh == false)
            return rec.persist;
        Debug.LogWarning($"Record with key '{key}' is not a persistent record or does not exist.");
        return null;
    }
    public static void EndSession()
    {
        foreach (var rec in records.Values) rec.session = null;
        isDirty = true;
        Save();
    }

    public static void LoadFromFile()
    {
        if (File.Exists(SavePath) == false)
        {
            records = new Dictionary<string, Records>();
            return;
        }
        var json = File.ReadAllText(SavePath);
#if UNITY_EDITOR
        Debug.Log($"records: {json}");
#endif
        try
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            records = JsonConvert.DeserializeObject<Dictionary<string, Records>>(json, settings);
            isDirty = false;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load records from file: {e.Message}");
            records = new Dictionary<string, Records>();
        }
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
        Debug.Log(sb);
    }
}