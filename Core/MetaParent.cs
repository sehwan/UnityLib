using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[Serializable]
public class MetaURL
{
    public string name;
    public string dev;
    public string rel;
}

public class MetaParent : MonoBehaviour
{
    public static MetaParent i;
    static readonly JsonSerializerSettings jsonSetting = new() { NullValueHandling = NullValueHandling.Ignore };
    int counter;
    [Immutable] public string mode = "";

    public string dev_sheet_URL;
    public string rel_sheet_URL;
    public MetaURL[] urls;


    public void LoadGoogleSheet(bool devMode = false)
    {
        counter = 0;
        mode = devMode ? "DEV" : "RELEASE";
        Debug.Log($"Loading {mode}");
        foreach (var e in urls) LoadParseInit(e, devMode);
    }

    async public void LoadParseInit(MetaURL e, bool devMode)
    {
        var url = devMode ? e.dev : e.rel;
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
        var json = CSVToJSON(req.downloadHandler.text);
        // print(json);
        Debug.Log(StringEx.GetDiffFromJson(old, json));
        fieldInfo.SetValue(this, JsonConvert.DeserializeObject(json, fieldType, jsonSetting));

        counter++;
        var color = devMode ? "orange" : "cyan";
        if (counter == urls.Length) Debug.Log($"Loaded {mode}".TagColor(color));
    }


    void Awake()
    {
        i = this;
    }

    static string CSVToJSON(string csv)
    {
        var ARRAY_DELIMITER = "/";
        var STARTING_ROW = 2;
        var dic = new Dictionary<string, JObject>();
        var rows = csv.Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var keys = rows[STARTING_ROW - 1].Split(',');
        // Row
        for (int i = STARTING_ROW; i < rows.Length; i++)
        {
            var row = rows[i].Split(',');
            // var jo = new Dictionary<string, object>();
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
                    // if (jo[key][names[0]] == null) jo[key][names[0]] = new Dictionary<string, object>();
                    // Array cell
                    if (names[1].Contains("arr_"))
                        jo[key][names[0]][names[1]] = JArray.FromObject(cell.Split(ARRAY_DELIMITER));
                    else jo[key][names[0]][names[1]] = cell;
                }
                // Normal
                else if (int.TryParse(cell, out int intValue)) jo[key] = intValue;
                else if (float.TryParse(cell, out float floatValue)) jo[key] = floatValue;
                else jo[key] = string.IsNullOrEmpty(cell) ? null : (JToken)cell;
            }
            dic.Add(jo[keys[0]].ToString(), jo);
        }
        return JsonConvert.SerializeObject(dic, jsonSetting);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Meta))]
public class MetaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var meta = target as MetaParent;

        GUILayout.Space(30);
        GUILayout.Label("Dev", EditorStyles.boldLabel);
        if (GUILayout.Button("Open Sheet", GUILayout.Height(40)))
            Application.OpenURL(meta.dev_sheet_URL);
        GUILayout.Space(10);
        if (GUILayout.Button("Load Dev", GUILayout.Height(60)))
            meta.LoadGoogleSheet(true);

        GUILayout.Space(30);
        GUILayout.Label("Normal", EditorStyles.boldLabel);
        if (GUILayout.Button("Open Sheet", GUILayout.Height(40)))
            Application.OpenURL(meta.rel_sheet_URL);
        GUILayout.Space(10);
        if (GUILayout.Button("Load Release", GUILayout.Height(60)))
            meta.LoadGoogleSheet(false);
    }
}
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