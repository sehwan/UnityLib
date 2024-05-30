using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;

// using Firebase.Database;
// using Firebase.Extensions;
// using Firebase.Auth;


public class UserBase : MonoBehaviour
{
    virtual public void UpdateDataForNewVersion() { }
    virtual public void OnRecord(string key) { }
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
    public UserData _;
    [JsonIgnore] public static UserData Data => i._;


    public static bool IsFilled() => i._ != null && i._.nick.IsFilled();
    public JObject xData;


    #region Application
    [Immutable] public DateTime dt_lastFocused = DateTime.Now;
    void OnApplicationQuit()
    {
        _.device = null;
        SaveImmediately();
    }
    void OnDestroy()
    {
        _.device = null;
        SaveImmediately();
    }
    void OnApplicationPause(bool paused)
    {
        if (paused == false) return;
        if (_.nick.IsNullOrEmpty()) return;
        _.device = null;
        dt_lastFocused = DateTime.Now;
        SaveImmediately();
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false) return;
        if (IsFilled() == false) return;
        if (_.nick.IsNullOrEmpty()) return;
        _.device = SystemInfo.deviceUniqueIdentifier;
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
        if (_ == null
            || _.nick.IsNullOrEmpty()
            // || data.nick == UserData.DefaultNick ||
            // data.IsTutorialOver() == false
            ) return;
        if (isForceQuit) return;

        _.dt_saved = DateTime.Now;

        if (isUsingServer) SaveToServer(isToast);
        else SaveToClient();
        print("saved at " + DateTime.Now);
    }
    static readonly string saveKey = "Reporter";
    static readonly string savePW = "shsh";
    void SaveToClient()
    {
        // try
        {
            var json = JsonConvert.SerializeObject(_);
            print(json);
            PlayerPrefs.SetString(saveKey, json);

            // fake
            var fake = DateTime.Now.ToString();
            PlayerPrefs.SetString("save", fake.Encrypt(fake));
        }
        // catch (Exception e)
        // {
        //     Debug.LogError(e);
        //     throw;
        // }
    }
    async void SaveToServer(bool isToast = false)
    {
        await NetworkMng.i.FuncAsync("user-save", isToast, ("data", JsonConvert.SerializeObject(_)));
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
            if ((DateTime.Now - _.dt_saved).TotalMinutes < Def.AutoSaveCycle) continue;
            // FirebaseMng.inst.TokenAsync();
            Save(false);
        }
    }

    public void BackUp()
    {
        NetworkMng.i.Func("user-backup", null, false, ("data", JsonConvert.SerializeObject(_)));
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
        _ = login.user;

        CheckBlackUser();
        UpdateDataForNewVersion();

        if (_.IsTutorialOver())
        {
            var diff = (login.now.ToDateTime() - _.dt_saved).TotalSeconds;
            Debug.Log($"<color=cyan>{diff / 60} min</color>");
            GetOfflineReward(diff);
            CheckDateChanged(login.now);
        }
        _.lastLogin = login.now;
        _.active = true;
        ListenDB();

        StartCoroutine(Co_AutoSave());
    }

    // public override void UpdateDataForNewVersion()
    // {
    // ETC
    // while (data.stageCur.Count < DataEx.GetEnumCount<StageMode>()) data.stageCur.Add(0);
    // // IAPs
    // foreach (var e in GameData.meta.iap)
    // {
    //     if (data.dts_iap.ContainsKey(e.Key) == false &&
    //         (e.Value.priceType == "time" || e.Value.duration != 0))
    //         data.dts_iap.Add(e.Key, DateTime.Now);
    // }
    // }

    void ListenDB()
    {
        if (_.IsTutorialOver() == false) return;
        if (_.dt_saved == DateTime.MinValue) return;
        if (isListening) return;
        // FirebaseMng.inst.ListenDB("users", FirebaseMng.inst.user.UserId, (shot) =>
        // {
        //     isListening = true;
        //     var changedData = shot.ToDictionary().Cast<Dictionary<string, object>, UserData>();

        //     // Black
        //     var black = changedData.black;

        //     // Device
        //     // var device = changedData.device;
        //     // if (device.IsFilled() && device != data.device)
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
        DateTime last = _.lastLogin.ToDateTime();
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
        if (_.black.IsFilled())
        {
            ToastGroup.Alert($"No permission : {_.black}");
            Application.Quit();
            return;
        }
        // FirebaseDatabase.DefaultInstance.GetReference($"black/{FirebaseMng.inst.user.UserId}")
        // .GetValueAsync().ContinueWith(task =>
        // {
        //     var r = task.Result.Value.ToString();
        //     data.black = r;
        //     if (r.IsFilled())
        //     {
        //         ToastGroup.Alert($"No Permission : {data.black}");
        //         Application.Quit();
        //         return;
        //     }
        // });
    }
    #endregion


    #region CheckDays
    public void OnDayChanged()
    {
        _.dq.Clear();
        _.att++;
        _.attCheck = false;

        // Attendance Reward
        var ticket = 3;
        RewardMsg.inst.Enqueue("Welcome!".L() + " " + _.att.ToOrdinal(), "ticket", ticket);
        AddResource("ticket", ticket);

        // VIP
        DateTime NOW = _.lastLogin.ToDateTime();
        foreach (var e in _.dts_iap)
        {
            // if (NOW <= e.Value &&
            //     GameData.meta.iap.ContainsKey(e.Key) &&
            //     GameData.meta.iap[e.Key].duration != 0)
            //     GameData.meta.iap[e.Key].GetReward();
        }

        // Check IAP Repurchasable
        // var filtered = GameData.meta.iap.Where(
        //     e => e.Value.day &&
        //     data.dts_iap.ContainsKey(e.Key));
        // NoticeRepurchasable(filtered);

        // FirebaseMng.Log("attend", "day", data.attend);
    }
    void OnWeekChanged()
    {
        // var filtered = GameData.meta.iap.Where(
        //     e => e.Value.week &&
        //     data.dts_iap.ContainsKey(e.Key));
        // NoticeRepurchasable(filtered);
    }
    void OnMonthChanged()
    {
        _.time_m = 0;
        _.onlineCheck = 0;
        // var filtered = GameData.meta.iap.Where(
        //     e => e.Value.month &&
        //     data.dts_iap.ContainsKey(e.Key));
        // NoticeRepurchasable(filtered);
    }
    // void NoticeRepurchasable(IEnumerable<KeyValuePair<string, IAPMeta>> filtered)
    // {
    //     foreach (var e in filtered)
    //     {
    //         data.dts_iap.Remove(e.Key);
    //         data.rcd_int.Remove(e.Key);
    //         ToastGroup.Show("YouCanRepurchase".LF(e.Key.L()));
    //     }
    // }

    public void ReportFinishTutorial()
    {
        _.tut = -1;
        ListenDB();

        // Reward
        int gem = 500;
        AddResource(Rsc._gem, gem);
        RewardMsg.inst.Enqueue("FinishTutorial".L(), Rsc._gem, gem);
    }
    #endregion


    #region Resources

    public void GetReward(Rsc r, string msg = "")
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
        if (_.rbox <= 0) return;
        var pn = UM.Get<GachaPanel>();
        var amount = Mathf.Min(_.rbox, pn.SlotCount);
        AddResource("rbox", -amount);
        RecordCumulative("rbox", amount);

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
                var reward = Rsc.New(Rsc._gold, 5);
                reward.InitToSlot(e.slot);
                AddResource(reward);
            }
            // Ticket
            else if (r == _++)
            {
                var reward = Rsc.New(Rsc._ticket, 1);
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
    public bool AddResource(Rsc r) => AddResource(r.t, r.n);
    public bool AddResource(string type, int value)
    {
        if (value == 0) return true;
        if (type == Rsc._soul)
        {
            Debug.LogError("Soul is BigNumber");
            return false;
        }

        var old = _.GetField<int>(type);
        if (old + value < 0 && value < 0)
        {
            ToastGroup.Show("NotEnoughResource".L());
            return false;
        }

        // if (value > 0) UM.Scene<MainScenePanel>().snowMsg.Show($"{value} {type.L()}");
        _.SetField(type, old + value);
        if (onAddResource.ContainsKey(type)) onAddResource[type](value);
        if (type == Rsc._gem) SaveImmediately();
        return true;
    }
    public bool AddResourceBig(Rsc r) => AddResourceBig(r.t, r.b);
    public bool AddResourceBig(string type, BigNumber value)
    {
        if (value == 0) return true;
        var old = _.GetField<BigNumber>(type);
        if (old + value < 0)
        {
            ToastGroup.Show("NotEnoughResource".L());
            return false;
        }
        // if (value > 0) UM.Scene<MainScenePanel>().snowMsg.Show($"{value} {type.L()}");
        _.SetField(type.ToString(), old + value);
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


    #region Record

    public void RecordGacha(int rate)
    {
        _.rcd_gacha.Add(rate);
        if (_.rcd_gacha.Count > 30) _.rcd_gacha.RemoveAt(0);
        _.gachaRate = (float)_.rcd_gacha.Average();
    }

    public void RecordtHigh(string type, int v)
    {
        var rcd = _.rcd_int.GetValueOrNew(type);
        if (rcd < v) _.rcd_int[type] = v;

        var meta = GameData.meta;
        if (meta.dq.ContainsKey(type))
        {
            var q = _.dq.GetValueOrNew(type);
            if (q.v < v) q.v = v;
        }
        if (meta.rq.ContainsKey(type))
        {
            var q = _.rq.GetValueOrNew(type);
            if (q.v < v) q.v = v;
        }

        // if (GameData.meta.achv.ContainsKey(type))
        // {
        //     var achv = data.achv[type];
        //     if (achv.val < v) achv.val = v;
        // }
        OnRecordBase(type);
    }
    public Action onChangeTask;
    public void RecordCumulative(string type, int add = 1)
    {
        if (_.rcd_int.ContainsKey(type) == false) _.rcd_int.Add(type, add);
        else _.rcd_int[type] += add;

        // Quest
        var meta = GameData.meta;
        if (meta.dq.ContainsKey(type)) _.dq.GetValueOrNew(type).v += add;
        if (meta.rq.ContainsKey(type)) _.rq.GetValueOrNew(type).v += add;

        // Task
        // var task = data.task;
        // if (task.key == type)
        // {
        //     task.val += add;
        //     task.val = Mathf.Min(task.val, task.req);
        //     if (onChangeTask != null) onChangeTask();
        // }

        // else if (GameData.meta.achv.ContainsKey(type))
        // {
        //     err = false;
        //     var achv = data.achv[type];
        //     achv.val += add;
        //     // print($"achv {type} {add}");
        // }
        OnRecordBase(type);
    }
    Action onRecord;
    void OnRecordBase(string type)
    {
        // All Cleared?
        // if (data.dailyQuest.All(e => e.Key == key_record_allClear || e.Value.fin))
        //     data.dailyQuest.GetValueDefault(key_record_allClear).val = 1;

        // Notify
        // var pn = UM.Get<QuestsPanel>();
        // if (pn.daily.ContainsKey(type)) pn.daily[type].RefreshAble();
        // if (pn.repeating.ContainsKey(type)) pn.repeating[type].RefreshAble();

        // var achv = UM.Get<UIAchievements>();
        // if (achv.items.ContainsKey(type)) achv.items[type].CheckAlarm(type);
        
        OnRecord(type);
        onRecord?.Invoke();
    }
    #endregion


    public Action onChangeNick;
    public void ReqChangeNick(bool canESC, Action cb = null)
    {
        int LIMIT_LENGTH = 14;
        var tempNick = _.nick == UserData.DefaultNick ? null : _.nick;
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
                this._.nick = newID;
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
        // var now = data.msg.IsFilled() ? data.msg : "-";

        // Common_InputField.inst.Show((string newSlogan) =>
        // {
        //     if (newSlogan.Length > LIMIT_LENGTH)
        //     {
        //         ToastGroup.Show("TooLong".L() + $" ({newSlogan.Length}/{LIMIT_LENGTH})");
        //         return;
        //     }

        //     data.msg = newSlogan;
        //     ToastGroup.Show("Complete".L());
        //     UM.Scene<UICamp>().RefreshUser();
        //     Common_InputField.inst.Hide();
        //     if (cb != null) cb();

        //     // FirebaseMng.Log("change_slogan");
        // }, msg, now, canESC, LIMIT_LENGTH);
    }



    public Action onChangeEnergy;
    public void ConsumeEnergy()
    {
        if (_.exEnergy > 0) _.exEnergy--;
        else _.dt_energy = _.dt_energy.GetTimeUsedChance(Def.EnergyCool, Def.EnergyMax + _.exEnergy);
        onChangeEnergy?.Invoke();
    }
    public void RefillEnergy(int token)
    {
        for (int i = 0; i < token; i++)
        {
            var nowCount = _.GetEnergyCount();
            if (Def.EnergyMax <= nowCount) _.exEnergy++;
            _.dt_energy = _.dt_energy.AddSeconds(-Def.EnergyCool);
        }
        onChangeEnergy?.Invoke();
    }


    public Action onChangeEXP;
    public void AddEXP(int n)
    {
        _.xp += n;
        if (_.lv >= Def.UserLevelMax) return;
        // var req = MetaUserLevel.GetNextReq(lv, Def.UserLevelMax);
        var req = _.lv * 10;
        if (req <= _.xp)
        {
            _.lv++;
            _.xp -= req;
            // User.i.ReportHigh("mxlv", lv);
            // Rewards
            // var msg = RewardsMsg.inst;
            // Energy
            // var count = 10;
            // RefillEnergy(count);
            // msg.InitToNextSlot("energy", count);
            // If Unlock? Then Get a Stone
            // if (lv < 10)
            // {
            //     var unlocking = Gamemeta.unitArr.Find(e => e.min_lv == lv);
            //     if (unlocking != null)
            //     {
            //         var stone = DataSStone.Generate(0, 1, 10, 0, unlocking.key);
            //         stone.InitToSlot(msg.GetNextSlot(), null, null);
            //         ssInven.Add(stone);
            //     }
            // }
            // Request Review
            // if (lv == 10) Common_StarRating.Show();
            // msg.Show("LevelUp!".L() + $" Lv.{lv}");
            // SFX.Play("levelup");
            // ETC
            // FirebaseMng.Log($"level{lv}");
        }
        onChangeEXP?.Invoke();
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(User), true)]
public class UserEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // if (GUILayout.Button("Save"))
        // {
        //     var user = target as User;
        //     user.SaveImmediately();
        // }
    }
}
#endif