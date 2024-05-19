using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkMng : MonoSingleton<NetworkMng>
{
    public bool isTestServer = true;
    [Immutable] public NetworkReachability status;

    // Route
    const string PROJECT_NAME = "berserker-e7aa8";
    readonly string root_rel = $"https://asia-northeast1-{PROJECT_NAME}.cloudfunctions.net/";
    readonly string root_dev = $"http://localhost:5001/{PROJECT_NAME}/asia-northeast1/";
    public string Root
    {
        get
        {
            if (Application.isMobilePlatform) isTestServer = false;
            if (isTestServer) return root_dev;
            else return root_rel;
        }
    }


    [SerializeField, Immutable] int _cnt_loading;
    public int cnt_loading
    {
        get { return _cnt_loading; }
        set
        {
            _cnt_loading = value;
            if (_cnt_loading >= 1) Loading.inst.Show();
            else Loading.inst.Hide();
        }
    }


    protected override void Awake()
    {
        status = Application.internetReachability;
    }
    void Update()
    {
        // if Changed
        var nowStatus = Application.internetReachability;
        if (nowStatus != status)
        {
            // Off
            if (nowStatus == NetworkReachability.NotReachable)
            {
                Loading.inst.Show("Need_Check_Internet".L());
                cnt_loading++;
            }
            // On
            else cnt_loading--;
        }
        status = nowStatus;
    }

    // Receipt Validation
    public async void ValidateReceipt(string receipt, Action cb)
    {
        var parsed = JObject.Parse(receipt);
        var payload = JObject.Parse(parsed["Payload"].ToString());
        var json = JObject.Parse(payload["json"].ToString());
        var res = await FuncAsync("iap-validateReceipt", true,
            ("orderId", json["orderId"]),
            ("purchaseToken", json["purchaseToken"]),
            ("productId", json["productId"]),
            ("packageName", json["packageName"])
        );
        if (res.IsOK()) cb();
    }

    public async void Log(string key, string value)
    {
        await FuncAsync("etc-log", false, (key, value));
    }


    // For Firebase
    public void Func(string uri, Action<string> cb, bool isCount, params (object, object)[] fields)
    {
        if (uri.EndsWith("/")) uri = $"{Root}{uri}";
        else uri = $"{Root}{uri}/";
        StartCoroutine(Co_Req(uri, cb, isCount, fields));
    }
    IEnumerator Co_Req(string uri, Action<string> cb, bool isCount, params (object, object)[] fields)
    {
        UnityWebRequest www;
        //Get
        if (fields == null) www = UnityWebRequest.Get(uri);
        //Post
        else
        {
            WWWForm form = new();
            foreach (var item in fields) form.AddField(item.Item1.ToString(), item.Item2.ToString());
            www = UnityWebRequest.Post(uri, form);
        }

        //Header
        www.timeout = 30;
        // www.SetRequestHeader("Authorization", FirebaseMng.inst.token);

        //Send and Wait
        DateTime dt = DateTime.Now;
        if (isCount) cnt_loading++;
        yield return www.SendWebRequest();
        if (isCount) cnt_loading--;

        //Callback
        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"{uri}\n{www.downloadHandler.text}");
            cb?.Invoke(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"{uri}\n{www.downloadHandler.text}");
            if (www.responseCode == 401)
            {
                // FirebaseMng.inst.TokenAsync();
                Func(uri, cb, isCount, fields);
            }
            else if (isCount) ToastGroup.Alert(www.downloadHandler.text);
        }
    }

    public async Task<UnityWebRequest> FuncAsync(string uri, bool isCount, params (object, object)[] fields)
    {
        UnityWebRequest www;
        var newURI = "";
        if (uri.EndsWith("/")) newURI = $"{Root}{uri}";
        else newURI = $"{Root}{uri}/";

        // Get
        if (fields == null) www = UnityWebRequest.Get(newURI);
        // Post
        else
        {
            WWWForm form = new();
            foreach (var item in fields) form.AddField(item.Item1.ToString(), item.Item2.ToString());
            www = UnityWebRequest.Post(newURI, form);
        }

        // Header
        www.timeout = 30;
        // www.SetRequestHeader("Authorization", FirebaseMng.inst.token);

        // Send and Wait
        if (isCount) cnt_loading++;
        var operation = www.SendWebRequest();
        while (!operation.isDone) await Task.Yield();
        if (isCount) cnt_loading--;

        // Result
        if (www.result == UnityWebRequest.Result.Success)
            Debug.Log($"<color=cyan>({uri})</color> {newURI}\n{www.downloadHandler.text}");
        else
        {
            Debug.LogError($"<color=cyan>{uri}|{www.responseCode}</color>{newURI}\n{www.error}\n{www.downloadHandler.text}");
            if (www.responseCode == 401)
            {
                // FirebaseMng.inst.TokenAsync();
            }
            else
            {
                if (isCount) ToastGroup.Alert($"Error: {www.downloadHandler.text}");
                else Debug.Log($"<color=cyan>{uri} {$"Error: {www.downloadHandler.text}"}</color>");
            }
        }
        return www;
    }

    // Just Web
    public async Task<UnityWebRequest> ReqAsync(string uri, bool isCount, params (object, object)[] fields)
    {
        UnityWebRequest www;
        var newURI = "";
        if (uri.EndsWith("/")) newURI = $"{uri}";
        else newURI = $"{uri}/";

        // Get
        if (fields == null) www = UnityWebRequest.Get(newURI);
        // Post
        else
        {
            WWWForm form = new();
            foreach (var item in fields) form.AddField(item.Item1.ToString(), item.Item2.ToString());
            www = UnityWebRequest.Post(newURI, form);
        }

        // Header
        www.timeout = 30;

        // Send and Wait
        if (isCount) cnt_loading++;
        var operation = www.SendWebRequest();
        while (!operation.isDone) await Task.Yield();
        if (isCount) cnt_loading--;

        // Result
        if (www.result == UnityWebRequest.Result.Success)
            Debug.Log($"<color=cyan>({uri})</color>{newURI}\n{www.downloadHandler.text}");
        else
        {
            if (isCount) ToastGroup.Alert($"Error:{www.responseCode} {www.error} {www.downloadHandler.text}");
            else Debug.Log($"<color=cyan>{uri} {$"Error:{www.responseCode} {www.error}  {www.downloadHandler.text}"}</color>");
        }
        return www;
    }
}
