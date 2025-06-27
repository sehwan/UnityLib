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


[Serializable]
public class RecordValue
{
    public object persist;
    public object session;
    public Type type;
    public bool isHigh;
    public bool isSteam;

    public bool IsHigh => isHigh;

    public RecordValue(Type type, bool isHigh, bool isSteam)
    {
        this.type = type;
        this.isHigh = isHigh;
        this.isSteam = isSteam;
        persist = null;
        session = null;
    }

    public void Operate(object value)
    {
        if (isHigh)
        {
            if (persist == null) persist = value;
            else if (type == typeof(int)) persist = Math.Max(Convert.ToInt32(persist), Convert.ToInt32(value));
            else if (type == typeof(long)) persist = Math.Max(Convert.ToInt64(persist), Convert.ToInt64(value));
            else if (type == typeof(float)) persist = Math.Max(Convert.ToSingle(persist), Convert.ToSingle(value));
            else if (type == typeof(double)) persist = Math.Max(Convert.ToDouble(persist), Convert.ToDouble(value));
        }
        else
        {
            if (persist == null)
            {
                session = value;
                persist = value;
            }
            else if (type == typeof(int))
            {
                session = Convert.ToInt32(session) + Convert.ToInt32(value);
                persist = Convert.ToInt32(persist) + Convert.ToInt32(value);
            }
            else if (type == typeof(long))
            {
                session = Convert.ToInt64(session) + Convert.ToInt64(value);
                persist = Convert.ToInt64(persist) + Convert.ToInt64(value);
            }
            else if (type == typeof(float))
            {
                session = Convert.ToSingle(session) + Convert.ToSingle(value);
                persist = Convert.ToSingle(persist) + Convert.ToSingle(value);
            }
            else if (type == typeof(double))
            {
                session = Convert.ToDouble(session) + Convert.ToDouble(value);
                persist = Convert.ToDouble(persist) + Convert.ToDouble(value);
            }
        }
        SetSteam(null); // key는 외부에서 전달
    }

    public object GetSession() => session;
    public object GetPersist() => persist;
    public object GetHigh() => persist;
    public void EndSession() => session = null;

    public void SetSteam(string key)
    {
#if STEAM
        if (isSteam == false || key == null) return;
        if (type == typeof(long))
            Steamworks.SteamUserStats.SetStat(key, Convert.ToInt64(persist));
        else if (type == typeof(int))
            Steamworks.SteamUserStats.SetStat(key, Convert.ToInt32(persist));
        else if (type == typeof(float) || type == typeof(double) )
            Steamworks.SteamUserStats.SetStat(key, Convert.ToSingle(persist));
#endif
    }
}

public static class Records
{
    static bool isDirty = false;
    static Dictionary<string, RecordValue> records = new();
    static readonly string SavePath = Path.Combine(UnityEngine.Application.persistentDataPath, "records.sav");

    public static void Model(string key, Type type, bool isHigh, bool isSteam = false)
    {
        if (!records.ContainsKey(key))
            records[key] = new RecordValue(type, isHigh, isSteam);
    }
    public static void Operate(string key, object value)
    {
        if (records.TryGetValue(key, out var rec))
        {
            rec.Operate(value);
            rec.SetSteam(key);
            isDirty = true;
        }
    }
    public static object GetHigh(string key)
    {
        if (records.TryGetValue(key, out var rec) && rec.IsHigh)
            return rec.GetHigh();
        return null;
    }
    public static object GetSession(string key)
    {
        if (records.TryGetValue(key, out var rec) && rec.IsHigh == false)
            return rec.GetSession();
        return null;
    }
    public static object GetPersist(string key)
    {
        if (records.TryGetValue(key, out var rec) && rec.IsHigh == false)
            return rec.GetPersist();
        return null;
    }
    public static void EndSession()
    {
        foreach (var rec in records.Values) rec.EndSession();
        isDirty = true;
    }
    public static void LoadFromFile()
    {
        if (!File.Exists(SavePath))
        {
            records = new Dictionary<string, RecordValue>();
            return;
        }
        var json = File.ReadAllText(SavePath);
        Debug.Log($"<color=green>{json}</color>");
        try
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            records = JsonConvert.DeserializeObject<Dictionary<string, RecordValue>>(json, settings);
            isDirty = false;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load records from file: {e.Message}");
            records = new Dictionary<string, RecordValue>();
        }
    }
    public static void Save()
    {
        if (!isDirty) return;
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
        StringBuilder o = new();
        foreach (var kv in records)
            o.AppendLine($"{kv.Key}: {kv.Value.type} {(kv.Value.IsHigh ? "High" : "Cumulative")} {kv.Value.GetSession()} / {kv.Value.GetPersist()} ");
        Debug.Log(o);
    }
}