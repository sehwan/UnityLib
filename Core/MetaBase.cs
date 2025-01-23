using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

[Serializable]
public class MetaGID
{
    public string name;
    public string dev_gid;
    public string rel_gid;
}

public class MetaBase : MonoBehaviour
{
    static readonly JsonSerializerSettings jsonSetting = new() { NullValueHandling = NullValueHandling.Ignore };
    int counter;
    [Immutable] public string currentMode = "";

    // public int STARTING_ROW = 2;

    [Header("Open Sheet")]
    public string dev_url_modify;
    public string rel_url_modify;

    [Header("Published")]
    public string dev_url_published;
    public string rel_url_published;
    public MetaGID[] gids;

    public string MakeURL(MetaGID urls, bool devMode)
    {
        var baseUrl = (devMode ? dev_url_published : rel_url_published).Split("/pubhtml")[0];
        var gid = devMode ? urls.dev_gid : urls.rel_gid;
        return $"{baseUrl}/pub?gid={gid}&single=true&output=csv";
    }
    public void LoadGoogleSheet(bool devMode = false)
    {
        counter = 0;
        currentMode = devMode ? "DEV" : "RELEASE";
        Debug.Log($"Loading {currentMode}");
        foreach (var e in gids) LoadParseInit(e, devMode);
    }

    async public void LoadParseInit(MetaGID e, bool devMode)
    {
        // print(e.name);
        var url = MakeURL(e, devMode);
        var fieldInfo = GetType().GetField(e.name);
        if (fieldInfo == null)
        {
            Debug.LogError($"Field '{e.name}' not found.");
            return;
        }
        var fieldType = fieldInfo.FieldType;
        var dic = fieldInfo.GetValue(this);
        var old = dic.ToJson();

        // Load Sheet
        var req = UnityWebRequest.Get(url);
        var oper = req.SendWebRequest();
        while (!oper.isDone) await Task.Delay(50);
        if (req.result == UnityWebRequest.Result.ConnectionError ||
            req.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(req.error);
            return;
        }
        // Parse
        Debug.Log($"<color=green>{e.name}</color>");
        var json = CSVToJSON(req.downloadHandler.text);
        print(json);
        Debug.Log(StringEx.GetDiffFromJson(old, json));
        fieldInfo.SetValue(this, JsonConvert.DeserializeObject(json, fieldType, jsonSetting));

        counter++;
        var color = devMode ? "orange" : "cyan";
        if (counter == gids.Length)
        {
            Debug.Log($"Loaded {currentMode}".TagColor(color));
            OnLoad();
        }
    }
    public virtual void OnLoad() { }


    string CSVToJSON(string csv)
    {
        print(csv);
        var ARRAY_DELIMITER = "/";
        var dic = new Dictionary<string, JObject>();
        var rows = csv.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var keyRow = 0;
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i].Split(',')[0] == "id")
            {
                keyRow = i;
                break;
            }
        }
        var keys = rows[keyRow].Split(',');
        keys = keys.Where(s => !string.IsNullOrEmpty(s)).ToArray();

        // Row
        for (int i = keyRow + 1; i < rows.Length; i++)
        {
            var row = rows[i].Split(',');
            if (row[0].IsNullOrEmpty()) break;

            JObject jo = new();
            // Cell
            for (int j = 0; j < keys.Length && j < row.Length; j++)
            {
                var key = keys[j];
                var cell = row[j];
                if (string.IsNullOrEmpty(cell)) jo[key] = null;
                // Array Cell
                else if (key.Contains("arr_"))
                {
                    jo[key] = JArray.FromObject(cell.Split(ARRAY_DELIMITER));
                }
                // Object Cell
                else if (key.Contains("obj_")) jo[key] = JsonConvert.DeserializeObject<JObject>(cell);
                // Nested Object
                else if (key.Contains("."))
                {
                    string[] names = key.Split('.');
                    // Array cell
                    if (names[1].Contains("arr_"))
                        jo[key][names[0]][names[1]] = JArray.FromObject(cell.Split(ARRAY_DELIMITER));
                    else jo[key][names[0]][names[1]] = cell;
                }
                // Normal
                else if (int.TryParse(cell, out int intValue)) jo[key] = intValue;
                else if (float.TryParse(cell, out float floatValue)) jo[key] = floatValue;
                else jo[key] = string.IsNullOrEmpty(cell) ? null : (JToken)cell;
                try
                {
                    dic[jo[keys[0]].ToString()] = jo;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    Debug.Log($"<color=green>{keys[0]}</color>");
                    Debug.Log($"<color=green>jo {jo}</color>");
                    Debug.Log($"<color=green>csv {csv}</color>");
                    throw;
                }
            }
        }
        return JsonConvert.SerializeObject(dic, jsonSetting);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(MetaBase), true)]
public class MetaBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var meta = target as MetaBase;

        GUILayout.Space(30);
        GUILayout.Label("Dev", EditorStyles.boldLabel);
        if (GUILayout.Button("Open Sheet", GUILayout.Height(40)))
            Application.OpenURL(meta.dev_url_modify);
        GUILayout.Space(10);
        if (GUILayout.Button("Load Dev", GUILayout.Height(60)))
            meta.LoadGoogleSheet(true);

        GUILayout.Space(30);
        GUILayout.Label("Normal", EditorStyles.boldLabel);
        if (GUILayout.Button("Open Sheet", GUILayout.Height(40)))
            Application.OpenURL(meta.rel_url_modify);
        GUILayout.Space(10);
        if (GUILayout.Button("Load Release", GUILayout.Height(60)))
            meta.LoadGoogleSheet(false);
    }
}

// ADD THIS TO META SCRIPT

// #if UNITY_EDITOR
// [CustomEditor(typeof(Meta))]
// public class MetaEditor : MetaEditorBase
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//     }
// }
// #endif

#endif

// Nested Object
// if (key.includes('.'))
// {
//     let names = key.split('.');
//     if (final[s][row.id][names[0]] == undefined) final[s][row.id][names[0]] = { }
//     // Array cell
//     if (names[1].includes('arr_'))
//         final[s][row.id][names[0]][names[1]] = cell.split(',');
//     else final[s][row.id][names[0]][names[1]] = cell;
// }

// // array cell
// else if (key.includes('arr_'))
// {
//     final[s][row.id][column] = cell.split(',')
//                         }
// // object cell
// else if (key.includes('obj_'))
// {
//     final[s][row.id][column] = JSON.parse(cell)
//                         }