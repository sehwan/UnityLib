using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// using Firebase.Database;
// using Firebase.Extensions;
// using Firebase.Auth;


public class UserBase : MonoBehaviour
{
    virtual public void UpdateDataForNewVersion() { }
}


public partial class User : UserBase
{
    public static User i;

    void Awake()
    {
        i = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("Settings")]
    [Immutable] public bool isUsingServer;
    [Immutable] public bool isCryptClient;
    [Immutable] public bool isBackUp;
    [Immutable] public bool isNewData;
    [Immutable] public bool isSkipTutorial;

    [Header("ETC")]
    [Immutable] public bool isListening;
    [Immutable] public bool isForceQuit;

    [Header("Data")]
    public UserData data;


    public static bool IsFilled() => i.data != null && i.data.nick.IsFilled();
    public JObject xData;


    #region Application
    [Immutable] public DateTime dt_lastFocused = DateTime.Now;
    void OnApplicationQuit()
    {
        data.device = null;
        SaveImmediately();
    }
    void OnDestroy()
    {
        data.device = null;
        SaveImmediately();
    }
    void OnApplicationPause(bool paused)
    {
        if (paused == false) return;
        if (data.nick.IsNullOrEmpty()) return;
        data.device = null;
        dt_lastFocused = DateTime.Now;
        SaveImmediately();
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false) return;
        if (IsFilled() == false) return;
        if (data.nick.IsNullOrEmpty()) return;
        data.device = SystemInfo.deviceUniqueIdentifier;
        // Diff
        var diff = DateTime.Now - dt_lastFocused;
        Debug.Log($"<color=cyan>{dt_lastFocused} {diff}</color>");
        if (diff.TotalSeconds > 0) Debug.Log($"<color=cyan>returned in {diff.ToFormattedString()}</color>");
        GetOfflineReward(Convert.ToUInt64(diff.TotalSeconds));
        dt_lastFocused = DateTime.Now;
    }
    #endregion


    #region Save
    // Interfaces
    public void SaveImmediately(bool isToast = false)
    {
        Save(isToast);
    }
    // After Purchasing IAP
    public void SaveAfterFrame(bool isToast = false)
    {
        if (Application.isEditor) Save(isToast);
        else this.InvokeEx(() => Save(isToast), 0.001f);
    }

    void Save(bool isToast)
    {
        if (data == null
        || data.nick.IsNullOrEmpty()
            // || _data.nick == UserData.DefaultNick ||
            // _data.IsTutorialOver() == false
            ) return;
        if (isForceQuit) return;

        data.dt_saved = DateTime.Now;

        if (isUsingServer) SaveToServer(isToast);
        else SaveToClient();
        print("saved at " + DateTime.Now);
    }
    static string saveKey = "Reporter";
    static string savePW = "shsh";
    void SaveToClient()
    {
        try
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(saveKey, json);

            // fake
            var fake = DateTime.Now.ToString();
            PlayerPrefs.SetString("save", fake.Encrypt(fake));
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }
    async void SaveToServer(bool isToast = false)
    {
        await NetworkMng.i.FuncAsync("user-save", isToast, ("data", JsonConvert.SerializeObject(data)));
        if (isToast) ToastGroup.Show("Complete".L());
        print("saved at " + DateTime.Now);
    }
    IEnumerator Co_AutoSave()
    {
        // yield return new WaitForSecondsRealtime(Def.AutoSaveCycle * 60);
        var w = new WaitForSecondsRealtime(30);
        while (true)
        {
            yield return w;
            if ((DateTime.Now - data.dt_saved).TotalMinutes < Def.AutoSaveCycle) continue;
            // FirebaseMng.inst.TokenAsync();
            Save(false);
        }
    }

    public void BackUp()
    {
        NetworkMng.i.Func("user-backup", null, false, ("data", JsonConvert.SerializeObject(data)));
    }
    #endregion


    #region Load
    DataLogin LoadFromClient()
    {
        var r = new DataLogin();
        if (PlayerPrefs.HasKey(saveKey))
        {
            var json = "";
            if (isCryptClient) json = PlayerPrefs.GetString(saveKey).Decrypt(savePW);
            else json = PlayerPrefs.GetString(saveKey);
            try
            {
                r.user = JsonConvert.DeserializeObject<UserData>(json);
                Debug.Log($"<color=cyan>{json}</color>");
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                r.user = UserData.Default();
                throw;
            }
        }
        else r.user = UserData.Default();
        r.now = DateTime.Now.ToLong();
        return r;
    }

    async Task<DataLogin> LoadFromServer()
    {
        var UID = SystemInfo.deviceUniqueIdentifier;
        var res = await NetworkMng.i.FuncAsync("user-load", true, ("uid", UID));
        if (res.IsError())
        {
            ToastGroup.Alert("Error");
            return null;
        }

        var login = new DataLogin();
        // New
        if (res.Text().ToObject<DataLogin>().user == null)
        {
            if (Application.isMobilePlatform) NetworkMng.i.Func("user-counter", null, false);
            login.user = UserData.Default();
            login.now = DateTime.Now.ToLong();
        }
        // Load
        else
        {
            login = JsonConvert.DeserializeObject<DataLogin>(res.Text());
            // Prevent Simultaneous Login
            var device = login.user.device;
            if (device.IsFilled() && device != UID)
            {
                ToastGroup.Alert("Simultaneouslogin".L());
                return null;
            }
        }
        login.user.device = UID;
        return login;
    }


    public async void LoadOrNew()
    {
        var login = new DataLogin();

        // Reset?
        if (Application.isEditor && isNewData)
        {
            login.now = DateTime.UtcNow.ToLong();
            login.user = UserData.Default();
            // No Tutorial?
            if (isSkipTutorial) login.user.tut = -1;
        }
        // Server
        else if (isUsingServer)
        {
            login = await LoadFromServer();
            if (login == null) return; // Make GM keep waiting
        }
        // Client
        else login = LoadFromClient();
        data = login.user;

        CheckBlackUser();
        UpdateDataForNewVersion();

        if (data.IsTutorialOver())
        {
            var diff = (login.now.ToDateTime() - data.dt_saved).TotalSeconds;
            Debug.Log($"<color=cyan>{diff / 60} min</color>");
            GetOfflineReward(diff);
            CheckDateChanged(login.now);
        }
        data.lastLogin = login.now;
        data.active = true;
        ListenDB();

        StartCoroutine(Co_AutoSave());
    }

    // public override void UpdateDataForNewVersion()
    // {
    // ETC
    // while (_data.stageCur.Count < DataEx.GetEnumCount<StageMode>()) _data.stageCur.Add(0);
    // // IAPs
    // foreach (var e in GameData.meta.iap)
    // {
    //     if (_data.dts_iap.ContainsKey(e.Key) == false &&
    //         (e.Value.priceType == "time" || e.Value.duration != 0))
    //         _data.dts_iap.Add(e.Key, DateTime.Now);
    // }
    // }

    void ListenDB()
    {
        if (data.IsTutorialOver() == false) return;
        if (data.dt_saved == DateTime.MinValue) return;
        if (isListening) return;
        // FirebaseMng.inst.ListenDB("users", FirebaseMng.inst.user.UserId, (shot) =>
        // {
        //     isListening = true;
        //     var changedData = shot.ToDictionary().Cast<Dictionary<string, object>, UserData>();

        //     // Black
        //     var black = changedData.black;

        //     // Device
        //     // var device = changedData.device;
        //     // if (device.IsFilled() && device != _data.device)
        //     // {
        //     //     ToastGroup.Alert("SimultaneousLogin".L());
        //     //     isForceQuit = true;
        //     //     Application.Quit();
        //     // }
        // });
        // FirebaseDatabase.DefaultInstance.GetReference($"msg/{FirebaseMng.inst.user.UserId}")
        // .ValueChanged += (sender, args) =>
        // {
        //     if (args.DatabaseError != null)
        //     {
        //         Debug.LogError(args.DatabaseError.Message);
        //         return;
        //     }
        //     if (args.Snapshot.Exists == false) return;

        //     var str = args.Snapshot.Value.ToString();
        //     if (str.IsNullOrEmpty()) return;
        //     if (str.StartsWith("mute/")) long.TryParse(str.Split("/")[1], out mute);
        //     else if (str.StartsWith("quit")) Application.Quit();
        // };
    }


    void GetOfflineReward(double diff)
    {
        var min = Mathf.Clamp((int)diff, 0, Def.LIMIT_AWAY_SEC) / 60;
        if (min == 0) return;
        // var val = Stage.ResourcePerMinute(User.data.Progress);
        // var gold = Resource.Gold(val.gold * min);
        // var soul = Resource.Soul(val.soul * min);
        // AddResource(gold);
        // AddResourceBig(soul);
        // // Reward Panel
        // var pn = RewardsMsg.inst;
        // soul.InitToSlot(pn.GetNextSlot());
        // gold.InitToSlot(pn.GetNextSlot());
        // pn.Show($"{"OfflineReward".L()}\n<color=cyan>{new TimeSpan(0, min, 0).ToFormattedShortString()}</color>");
        // FirebaseMng.Log("offline", "min", min);
    }
    void CheckDateChanged(ulong now)
    {
        // If the Day was Changed
        DateTime last = data.lastLogin.ToDateTime();
        // Day
        if (last.IsDateChangedFromNow())
        {
            OnDayChanged();
            // Backup into Firestore
            if (isBackUp) BackUp();
        }
        // Week 6 -> 0~5
        if (last.IsWeekChangedFromNow()) OnWeekChanged();
        // Month
        if (last.IsMonthChangedFromNow()) OnMonthChanged();
    }

    void CheckBlackUser()
    {
        if (data.black.IsFilled())
        {
            ToastGroup.Alert($"No permission : {data.black}");
            Application.Quit();
            return;
        }
        // FirebaseDatabase.DefaultInstance.GetReference($"black/{FirebaseMng.inst.user.UserId}")
        // .GetValueAsync().ContinueWith(task =>
        // {
        //     var r = task.Result.Value.ToString();
        //     _data.black = r;
        //     if (r.IsFilled())
        //     {
        //         ToastGroup.Alert($"No Permission : {_data.black}");
        //         Application.Quit();
        //         return;
        //     }
        // });
    }
    #endregion


    #region CheckDays
    public void OnDayChanged()
    {
        data.dq.Clear();
        data.att++;
        data.attCheck = false;

        // Attendance Reward
        var ticket = 3;
        RewardMsg.inst.Enqueue("Welcome!".L() + " " + data.att.ToOrdinal(), "ticket", ticket);
        AddResource("ticket", ticket);

        // VIP
        DateTime NOW = data.lastLogin.ToDateTime();
        foreach (var e in data.dts_iap)
        {
            // if (NOW <= e.Value &&
            //     GameData.meta.iap.ContainsKey(e.Key) &&
            //     GameData.meta.iap[e.Key].duration != 0)
            //     GameData.meta.iap[e.Key].GetReward();
        }

        // Check IAP Repurchasable
        // var filtered = GameData.meta.iap.Where(
        //     e => e.Value.day &&
        //     _data.dts_iap.ContainsKey(e.Key));
        // NoticeRepurchasable(filtered);

        // FirebaseMng.Log("attend", "day", _data.attend);
    }
    void OnWeekChanged()
    {
        // var filtered = GameData.meta.iap.Where(
        //     e => e.Value.week &&
        //     _data.dts_iap.ContainsKey(e.Key));
        // NoticeRepurchasable(filtered);
    }
    void OnMonthChanged()
    {
        data.time_m = 0;
        data.onlineCheck = 0;
        // var filtered = GameData.meta.iap.Where(
        //     e => e.Value.month &&
        //     _data.dts_iap.ContainsKey(e.Key));
        // NoticeRepurchasable(filtered);
    }
    // void NoticeRepurchasable(IEnumerable<KeyValuePair<string, IAPMeta>> filtered)
    // {
    //     foreach (var e in filtered)
    //     {
    //         _data.dts_iap.Remove(e.Key);
    //         _data.rcd_int.Remove(e.Key);
    //         ToastGroup.Show("YouCanRepurchase".LF(e.Key.L()));
    //     }
    // }

    public void ReportFinishTutorial()
    {
        data.tut = -1;
        ListenDB();

        // Reward
        int gem = 500;
        AddResource(Resource.kGem, gem);
        RewardMsg.inst.Enqueue("FinishTutorial".L(), Resource.kGem, gem);
    }
    #endregion


    #region Resources

    public void GetReward(Resource r, string msg = "")
    {
        // if (r.r == Resource.kSoul)
        // {
        //     if (r.b != 0) AddResourceBig(r);
        //     else
        //     {
        //         if (r.n > 100) Debug.LogError($"<color=cyan>Reward is too much : {r.n}</color>");
        //         r.b = new BigNumber(r.n * Stage.ResourcePerMinute(User.data.Progress).soul);
        //         AddResourceBig(r);
        //     }
        // }
        // else AddResource(r);
        // RewardMsg.inst.Enqueue(msg, r);
        // SFX.Play("loot");
    }

    public void OpenRbox()
    {
        if (data.rbox <= 0) return;
        var pn = UM.Get<GachaPanel>();
        var amount = Mathf.Min(data.rbox, pn.SlotCount);
        AddResource("rbox", -amount);
        RecordInt("rbox", amount);

        // Chance
        int[] CHANCE = new int[] {
            // Gold, Soul, Key
            26, 26, 3,
            // Equip, Artifact, God, Spirit
            9, 12, 12, 12
        };
        for (int i = 0; i < amount; i++)
        {
            var e = pn.slots[i];
            var r = CHANCE.Gatcha();
            var _ = 0;
            // Gold
            if (r == _++)
            {
                var reward = Resource.Gold(5);
                reward.InitToSlot(e.slot);
                AddResource(reward);
            }
            // Ticket
            else if (r == _++)
            {
                var reward = Resource.Ticket(1);
                reward.InitToSlot(e.slot);
                AddResource(reward);
            }
        }
        SaveImmediately();
        pn.Show(amount, "Random Box".L() + $" x{amount}");
    }

    public Dictionary<string, Action<int>> onAddResource =
        new();
    public Dictionary<string, Action<BigNumber>> onAddResourceBig =
        new();
    public bool AddResource(Resource r) => AddResource(r.r, r.n);
    public bool AddResource(string type, int value)
    {
        if (value == 0) return true;
        if (type == Resource.kSoul)
        {
            Debug.LogError("Soul is BigNumber");
            return false;
        }

        var old = data.GetField<int>(type);
        if (old + value < 0 && value < 0)
        {
            ToastGroup.Show("NotEnoughResource".L());
            return false;
        }

        // if (value > 0) UM.Scene<MainScenePanel>().snowMsg.Show($"{value} {type.L()}");
        data.SetField(type, old + value);
        if (onAddResource.ContainsKey(type)) onAddResource[type](value);
        if (type == Resource.kGem) SaveImmediately();
        return true;
    }
    public bool AddResourceBig(Resource r) => AddResourceBig(r.r, r.b);
    public bool AddResourceBig(string type, BigNumber value)
    {
        if (value == 0) return true;
        var old = data.GetField<BigNumber>(type);
        if (old + value < 0)
        {
            ToastGroup.Show("NotEnoughResource".L());
            return false;
        }
        // if (value > 0) UM.Scene<MainScenePanel>().snowMsg.Show($"{value} {type.L()}");
        data.SetField(type.ToString(), old + value);
        if (onAddResourceBig.ContainsKey(type)) onAddResourceBig[type](value);
        return true;
    }
    public void AddResourceCallback(string type, Action<int> callback)
    {
        if (onAddResource.ContainsKey(type))
            onAddResource[type] += callback;
        else
            onAddResource.Add(type, callback);
    }
    public void RemoveResourceCallback(string type, Action<int> callback)
    {
        if (onAddResource.ContainsKey(type))
            onAddResource[type] -= callback;
    }
    public void AddResourceBigCallback(string type, Action<BigNumber> callback)
    {
        if (onAddResourceBig.ContainsKey(type))
            onAddResourceBig[type] += callback;
        else
            onAddResourceBig.Add(type, callback);
    }
    public void RemoveResourceBigCallback(string type, Action<BigNumber> callback)
    {
        if (onAddResourceBig.ContainsKey(type))
            onAddResourceBig[type] -= callback;
    }
    #endregion


    #region Quest & Achievement        
    public void ReportHigh(string type, int v)
    {
        var meta = GameData.meta;
        if (meta.dq.ContainsKey(type))
        {
            var q = data.dq.GetValueOrNew(type);
            if (q.v < v) q.v = v;
        }
        if (meta.rq.ContainsKey(type))
        {
            var q = data.rq.GetValueOrNew(type);
            if (q.v < v) q.v = v;
        }

        // if (GameData.meta.achv.ContainsKey(type))
        // {
        //     var achv = data.achv[type];
        //     if (achv.val < v) achv.val = v;
        // }
        OnReport(type);
    }
    public Action onChangeTask;
    public void ReportAccrued(string type, int add = 1)
    {
        // Quest
        var meta = GameData.meta;
        if (meta.dq.ContainsKey(type)) data.dq.GetValueOrNew(type).v += add;
        if (meta.rq.ContainsKey(type)) data.rq.GetValueOrNew(type).v += add;

        // Task
        // var task = _data.task;
        // if (task.key == type)
        // {
        //     task.val += add;
        //     task.val = Mathf.Min(task.val, task.req);
        //     if (onChangeTask != null) onChangeTask();
        // }

        // else if (GameData.meta.achv.ContainsKey(type))
        // {
        //     err = false;
        //     var achv = _data.achv[type];
        //     achv.val += add;
        //     // print($"achv {type} {add}");
        // }
        OnReport(type);
    }
    void OnReport(string type)
    {
        // All Cleared?
        // if (_data.dailyQuest.All(e => e.Key == key_record_allClear || e.Value.fin))
        // {
        //     _data.dailyQuest.GetValueDefault(key_record_allClear).val = 1;
        // }

        // Notify
        var pn = UM.Get<QuestsPanel>();
        if (pn.daily.ContainsKey(type)) pn.daily[type].RefreshAble();
        if (pn.repeating.ContainsKey(type)) pn.repeating[type].RefreshAble();

        // var achv = UM.Get<UIAchievements>();
        // if (achv.items.ContainsKey(type)) achv.items[type].CheckAlarm(type);
    }
    #endregion


    // to Check if user is a Cheater
    public void RecordInt(string type, int add = 1)
    {
        if (data.rcd_int.ContainsKey(type) == false) data.rcd_int.Add(type, add);
        else data.rcd_int[type] += add;
    }

    public Action onChangeNick;
    public void ReqChangeNick(bool canESC, Action cb = null)
    {
        int LIMIT_LENGTH = 14;
        var tempNick = data.nick == UserData.DefaultNick ? null : data.nick;
        Common_InputField.i.Show((Action<string>)(async (string newID) =>
        {
            if (newID.Length < 2)
            {
                ToastGroup.Show("TooShort".L() + $"(2 ~ {LIMIT_LENGTH})");
                return;
            }
            if (newID.Length > LIMIT_LENGTH)
            {
                ToastGroup.Show("TooLong".L() + $"(2 ~ {LIMIT_LENGTH}chars)");
                return;
            }
            if (Def.RESERVED_NAMES.Contains(newID))
            {
                ToastGroup.Show("NotAvailable".L());
                return;
            }
            // var reg = new Regex(@"^.[a-z|A-Z|0-9|\*]{1,14}$");
            // var reg = new Regex("^[a-zA-Z0-9]*$");
            // var reg = new Regex(@"^.[ㄱ-ㅎ|가-힣|a-z|A-Z|0-9|\*]{1,14}$");
            // var reg = new Regex(@"^[ㄱ-ㅎ|가-힣|a-z|A-Z|0-9|]*$");
            var reg = new Regex(@"^[\p{L}\p{N}ぁ-んァ-ン一-龯가-힣々〆ヵヶ]{1,14}$");

            if (reg.IsMatch(newID) == false)
            {
                ToastGroup.Show("NotAvailable".L());
                return;
            }

            var res = await NetworkMng.i.FuncAsync("user-isNickOK", true, ((object, object))("newID", newID));
            if (res.IsOK())
            {
                ToastGroup.Show("Complete".L());
                Common_InputField.i.Hide();
                this.data.nick = newID;
                cb?.Invoke();
                onChangeNick?.Invoke();
            }
            // }), "InputNewName".L() + "\n" + $"({"OnlyAlphabetsAndNumbers".L()})", tempNick, canESC, LIMIT_LENGTH);
            // }), "InputNewName".L() + "\n" + $"({"NotAvailableCharacters".L()})", tempNick, canESC, LIMIT_LENGTH);
        }), "InputNewName".L(), (string)tempNick, canESC, LIMIT_LENGTH);
    }

    public void ChangeSlogan(bool canESC, Action cb = null)
    {
        // int LIMIT_LENGTH = 30;
        // var msg = "ChangeTheSlogan".L();
        // var now = _data.msg.IsFilled() ? _data.msg : "-";

        // Common_InputField.inst.Show((string newSlogan) =>
        // {
        //     if (newSlogan.Length > LIMIT_LENGTH)
        //     {
        //         ToastGroup.Show("TooLong".L() + $" ({newSlogan.Length}/{LIMIT_LENGTH})");
        //         return;
        //     }

        //     _data.msg = newSlogan;
        //     ToastGroup.Show("Complete".L());
        //     UM.Scene<UICamp>().RefreshUser();
        //     Common_InputField.inst.Hide();
        //     if (cb != null) cb();

        //     // FirebaseMng.Log("change_slogan");
        // }, msg, now, canESC, LIMIT_LENGTH);
    }

    public void RecordGacha(int rate)
    {
        data.rcd_gacha.Add(rate);
        if (data.rcd_gacha.Count > 30) data.rcd_gacha.RemoveAt(0);
        data.gachaRate = (float)data.rcd_gacha.Average();
    }


    #region Properties

    public Action onChangeEXP;
    public void AddEXP(int n)
    {
        // Business
        // var req = MetaUserLevel.GetNextReq(_data.lv, Def.UserLevelMax);
        // _data.exp = Mathf.Clamp(_data.exp + n, 0, (int)req);
        // if (_data.fameHi < _data.exp) _data.fameHi = _data.exp;

        // // Level Up?
        // if (_data.lv < Def.UserLevelMax &&
        //     req <= _data.exp)
        // {
        //     // Level
        //     _data.lv++;
        //     ReportHigh("mxlv", _data.lv);
        //     // Rewards
        //     var msg = RewardsMsg.inst;
        //     // Energy
        //     var count = 10;
        //     RefillEnergy(count);
        //     msg.InitToNextSlot("energy", count);
        //     // Manas
        //     var gold = _data.lv * 10000;
        //     AddGold(gold);
        //     msg.InitToNextSlot("mana", gold);
        //     msg.InitToNextSlot("dark", gold);
        //     // If Unlock? Then Get a Stone
        //     if (_data.lv < 10)
        //     {
        //         var unlocking = GameData.meta.unitArr.Find(e => e.min_lv == _data.lv);
        //         if (unlocking != null)
        //         {
        //             var stone = DataSStone.Generate(0, 1, 10, 0, unlocking.key);
        //             stone.InitToSlot(msg.GetNextSlot(), null, null);
        //             _data.ssInven.Add(stone);
        //         }
        //     }
        //     // Request Review
        //     if (_data.lv == 10) Common_StarRating.Show();
        //     msg.Show("LevelUp!".L() + $" Lv.{_data.lv}");
        //     SFX.Play("levelup");
        //     // ETC
        //     FirebaseMng.Log($"level{_data.lv}");
        // }

        onChangeEXP?.Invoke();
    }
    #endregion
}