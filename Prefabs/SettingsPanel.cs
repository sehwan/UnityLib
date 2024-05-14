using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public Toggle tg_pushnoti;
    public Toggle tg_vibrate;
    public Toggle tg_burnin;
    public Toggle tg_bgm;
    public Slider sl_bgm;
    public Toggle tg_sfx;
    public Slider sl_sfx;
    public Button btn_term;
    public Button btn_community;
    public Text txt_version;
    public GameObject pn_language;

    public static void Show()
    {
        var _ = Instantiate(Resources.Load<GameObject>("Prefabs/SettingsPanel")).GetComponent<SettingsPanel>();
        if (Application.isMobilePlatform)
        {
            _.tg_vibrate.isOn = Vibration.NoVibrate;
            _.tg_burnin.isOn = Settings.NoScreenSaver;
        }
        _.sl_bgm.value = Settings.VolumeBGM;
        _.sl_sfx.value = Settings.VolumeSFX;
        _.txt_version.text = $"v{Application.version}";
        _.pn_language.SetActive(false);
        // bool isKorean = LocalizationMng.inst.IsKorean();
        // btn_term.SetActive(isKorean);
        // btn_cafe.SetActive(isKorean);
    }
    void Awake()
    {
        pn_language.transform.ForEach(e =>
        {
            e.GetComponentInChildren<Text>().text = e.name;
        });
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Btn_Close();
    }
    public void Btn_Close()
    {
        Destroy(gameObject);
    }

    #region Cheat
    public int count_cheat;
    public void Btn_Cheat()
    {
        count_cheat++;
        if (Application.isEditor && count_cheat < 3) return;
        if (Application.isMobilePlatform/*  && count_cheat < 15 */) return;
        count_cheat = 0;
        Common_InputField.i.Show(r =>
        {
            CheckCheatKey(r);
            Common_InputField.i.Hide();
        }, "");
    }

    async void CheckCheatKey(string s)
    {
        if (Application.isMobilePlatform) return;
        if (s == "async")
        {
            await System.Threading.Tasks.Task.Delay(1000);
            Debug.Log("Async Done");
        }
        if (s == "1212level")
        {
            // var next = MetaUserLevel.GetNextReq(User.data.lv);
            // User.inst.AddEXP((int)next);
        }
        else if (s == "1212money")
        {
            // User.inst.AddDark(1000000);
        }
        // else if (s == "1212date") User.inst.OnDayChanged();
        else if (s == "1212reset")
        {
            User.i._data = UserData.Default();
            Application.Quit();
        }
        else if (s == "1212sstone")
        {
            // for (int i = 0; i < 50; i++)
            // {
            //     int m = 0;
            //     int d = 0;
            //     if (50.Chance()) m = 100;
            //     else d = 100;
            //     var item = DataSStone.Generate(3, 4, m, d,
            //         GameData.meta.unitArr.FindAll(e => e.human == false).Count().Random());
            //     User.data.ssInven.Add(item);
            // }
        }
        else return;
        // FirebaseMng.Log("cheat", "key", s, "nick", User.data.nick);
        ToastGroup.Show("!");
    }
    #endregion

    public virtual void On_Pushnoti(bool b)
    {
        Settings.NoPushnoti = b;
        // if (b) Firebase.Messaging.FirebaseMessaging.UnsubscribeAsync("game");
        // else Firebase.Messaging.FirebaseMessaging.SubscribeAsync("game");
    }

    public void On_Vibrate(bool b)
    {
        Vibration.NoVibrate = b;
    }

    public void On_ScreenSaver(bool b)
    {
        Settings.NoScreenSaver = b;
    }

    public void OnMuteBGM(bool b)
    {
        Settings.MuteBGM = b;
        BGM.Mute(b);
    }
    public void OnMuteSFX(bool b)
    {
        Settings.MuteSFX = b;
        SFX.Mute(b);
    }
    public void OnValueBGM(float v)
    {
        Settings.VolumeBGM = v;
        BGM.SetVolume(v);
        if (v == 0) OnMuteBGM(true);
        else OnMuteBGM(false);
    }
    public void OnValueSFX(float v)
    {
        Settings.VolumeSFX = v;
        SFX.SetVolume(v);
        if (v == 0) OnMuteSFX(true);
        else OnMuteSFX(false);
    }

    public void Btn_ChangeLanguage()
    {
        pn_language.SetActive(true);
        // Common_LittleMenu.Show(
        //     300, 45,
        //     ("English", () => StringEx.ChangeLanguage("English")),
        //     ("한국어", () => StringEx.ChangeLanguage("Korean")),
        //     ("简体中文", () => StringEx.ChangeLanguage("Chinese (Simplified)")),
        //     ("繁體中文", () => StringEx.ChangeLanguage("Chinese (Traditional)")),
        //     ("Русский", () => StringEx.ChangeLanguage("Russian")),
        //     ("Português", () => StringEx.ChangeLanguage("Portuguese")),
        //     ("Português (Brasil)", () => StringEx.ChangeLanguage("Portuguese (Brazil)")),
        //     ("日本語", () => StringEx.ChangeLanguage("Japanese")),
        //     ("Español", () => StringEx.ChangeLanguage("Spanish")),
        //     ("Français", () => StringEx.ChangeLanguage("French")),
        //     ("Deutsch", () => StringEx.ChangeLanguage("German")),
        //     ("Italiano", () => StringEx.ChangeLanguage("Italian")),
        //     ("Polski", () => StringEx.ChangeLanguage("Polish")),
        //     ("Türkçe", () => StringEx.ChangeLanguage("Turkish")),
        //     ("ไทย", () => StringEx.ChangeLanguage("Thai"))
        // );
    }
    public void ChangeLanguage(GameObject go)
    {
        StringEx.ChangeLanguage(go.name);
        pn_language.SetActive(false);
    }
    public void Btn_Community()
    {
        // ToastGroup.Show("준비중입니다.");
        Application.OpenURL("https://cafe.naver.com/guildmastermobile");
    }
    public void Btn_Terms()
    {
        Application.OpenURL("https://sites.google.com/view/alchemists-terms/%ED%99%88");
    }

    public void Btn_Share()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string subject = "App_Name".L();
        string body = Def.URL_Market;
        using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent"))
        using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent"))
        {
            intentObject.Call<AndroidJavaObject>("setAction", intentObject.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_SUBJECT"), subject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_TEXT"), body);
            using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via"))
                currentActivity.Call("startActivity", jChooser);
        }
#endif
    }

    public void Btn_Inquiry()
    {
        string email = "alchemistsgames@gmail.com";
        string subject = MyEscapeURL($"Inquiry {Application.productName} {User.data.nick}");
        string body = MyEscapeURL("Please Enter Your Message here\n\n\n\n" +
        "Project: " + Application.productName + "\n" +
          "Name: " + User.data.nick + "\n" +
         "Model: " + SystemInfo.deviceModel + "\n" +
            "OS: " + SystemInfo.operatingSystem + "\n" +
        "Version:" + Application.version);
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
    string MyEscapeURL(string url)
    {
        return UnityEngine.Networking.UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }



    public void Btn_FrameRate()
    {
        PopNumberInput.Show("Framerate".L() + " (30~60)", 30, 60, 1, r =>
        {
            Application.targetFrameRate = (int)r;
            Settings.FrameRate = (int)r;
        }, Settings.FrameRate);
    }

    public void Btn_Save()
    {
        User.i.SaveImmediately(true);
    }
    public void Btn_QuitGame()
    {
        // User.data.device = null;
        // User.i.SaveImmediately();
        Application.Quit();
    }

    public void Btn_Coupon()
    {
        // Common_InputField.inst.Show(r =>
        // {
        //     if (User.data.coupons.IndexOf(r) >= 0)
        //     {
        //         ToastAlert.inst.Show("이미 사용한 쿠폰입니다.".Localize());
        //         return;
        //     }
        //     if (GameData.meta.coupons.ContainsKey(r) == false)
        //     {
        //         ToastAlert.inst.Show("존재하지 않는 쿠폰입니다.".Localize());
        //         return;
        //     }
        //     //Reward
        //     User.data.coupons.Add(r);
        //     var reward = GameData.meta.coupons[r];
        //     User.inst.GetReward(reward, "쿠폰 보상!".Localize());
        //     Common_InputField.inst.Hide();
        //     FirebaseMng.Log("coupon", "key", r);
        // }, "Coupon");
    }

    public const int MinStageForRegistRecommender = 20;
    public const int RewardForRecommender = 300;
    public const int RewardForRegister = 300;
    public void Btn_Recommender()
    {
        // if (User.data.didFillRecommender)
        // {
        //     ToastGroup.Show("You have already registered a recommender".L());
        //     return;
        // }

        // Common_InputField.inst.Show(r =>
        // {
        //     if (r.IsNullOrEmpty())
        //     {
        //         ToastGroup.Show("Empty".L());
        //         return;
        //     }
        //     if (User.data.nick == r)
        //     {
        //         ToastGroup.Show("YouCannotRecommendYourself".L());
        //         return;
        //     }
        //     // if (User.data.Progress < MinStageForRegistRecommender)
        //     // {
        //     //     ToastGroup.Show("It'sPossibleFrom{0}".LF(MinStageForRegistRecommender));
        //     //     return;
        //     // }

        //     NetworkMng.inst.Func("user-fillRecommender", (json =>
        //     {
        //         User.data.didFillRecommender = true;
        //         // Reward
        //         User.inst.GetReward(
        //             Resource.Gem(RewardForRegister),
        //             "Reward".L());
        //         Common_InputField.inst.Hide();
        //         // FirebaseMng.Log("recommender");
        //     }), true,
        //         ("recommender", r),
        //         ("user", User.data.nick));
        // },
        //     "EnterTheRecommender'sNickname".L() +
        //     "\n\n" + "RecommenderGets{0:n0}Gems".LF(RewardForRecommender).TagSize(22) +
        //     "\n" + "RegistererGets{0:n0}Gems".LF(RewardForRegister).TagSize(22) +
        //     "\n" + "AvailableFrom{0}OrHigher".LF(MinStageForRegistRecommender).TagSize(22)
        // );
    }


    public int countWake;
    public void WakeReporter()
    {
        countWake++;
        if (countWake <= 20) return;
        // GameObject.Find("Reporter").GetComponent<Reporter>().enabled = true;
        // FirebaseMng.Log("wake_reporter", "count", countWake);
    }

    public void Btn_Notice()
    {
        string lang = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        if (lang == "ko")
            Application.OpenURL("https://regular-stretch-f51.notion.site/84797172aab4473185eff2aed60d63bd");
        else if (lang == "ja")
            Application.OpenURL("https://regular-stretch-f51.notion.site/2e06a54abdcf4702900f3f3718c02450");
        else
            Application.OpenURL("https://regular-stretch-f51.notion.site/Notice-049cb4b9ca574a9eb696eae6aa1dd89a");
        // FirebaseMng.Log("notice_" + lang);
    }

    public void Btn_Games()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=AlchemistsGames");
    }
}