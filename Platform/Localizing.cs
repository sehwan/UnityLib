using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Localizing : MonoBehaviour
{
    const string SheetPath =
    "https://docs.google.com/spreadsheets/d/1Cuv5dq_GJUJdcClWOOKl5aetBEXwb7xoJZrpmsCemfQ";
    const string URL = SheetPath + "/export?format=tsv";
    public static Dictionary<string, Dictionary<string, string>> all = new();
    public static Dictionary<string, string> cur = new();
    public List<string> list;
    [TextArea(10, 10)] public string rawText;

    public static Localizing inst;
    void Awake()
    {
        if (null == inst)
        {
            inst = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
        ToDictionary();
        cur = all[PlayerPrefs.GetString("language", "English")];
    }
    void ToDictionary()
    {
        string[] rows = rawText.Split('\n');
        int rowLen = rows.Length;
        int colLen = rows[0].Split('\t').Length;

        var colKeys = rows[0].Split('\t');
        for (int i = 1; i < colLen; i++)
        {
            all.Add(colKeys[i], new Dictionary<string, string>());
        }

        for (int i = 0; i < rowLen; i++)
        {
            string[] row = rows[i].Split('\t');
            for (int j = 1; j < colLen; j++)
            {
                all[colKeys[j]].Add(row[0], row[j]);
            }
        }
    }

    public string Loc(string origin)
    {
        return cur[origin];
    }

    [ContextMenu("Import")]
    async void Import()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        var operation = www.SendWebRequest();
        while (!operation.isDone) await Task.Yield();
        rawText = www.downloadHandler.text;
        string[] rows = rawText.Split('\n');
        list = rows.ToList();
    }
}