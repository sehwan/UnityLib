using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class AttendancePanel : UIWindow
{
    public Transform tr_table;
    public Text txt_title;

    [Header("User Data Field / MetaData Dictionary / Noti Badge")]
    // ex) User.data."attend"
    public string key;

    // ex) User.data."attendCheck"
    public string keyCheck => $"{key}Check";

    [Header("Reward Name")]
    public string title;

    int CYCLE => tr_table.childCount;


    public override void Init()
    {
        base.Init();
        txt_title.text = title.L();
        RefreshToday();
        // if (User.data.IsTutorialOver() == false) return;
        // if (User.data.GetField<bool>(keyCheck)) return;
        Show();
    }
    public override void Show()
    {
        // base.Show();
        // int day = (User.data.GetField<int>(key) % CYCLE);
        // var table = GameData.meta.GetField<Dictionary<int, Resource>>(key);
        // for (int i = 0; i < tr_table.childCount; i++)
        // {
        //     var c = tr_table.GetChild(i);
        //     var reward = table[i];
        //     c.Find("txt_day").GetComponent<Text>().text = (i + 1).ToString();
        //     c.Find("img_icon").GetComponent<Image>().sprite = UIUtil.GetIcon(reward.r);
        //     c.Find("txt_count").GetComponent<Text>().text = "x" + reward.n.ToString("n0");
        //     c.Find("img_check").SetActive(false);
        //     c.Find("today").SetActive(false);

        //     if (i < day) SetPassed(c);
        //     else if (i == day) RefreshToday();
        //     else
        //     {
        //     }
        // }
    }

    void SetPassed(Transform tr)
    {
        tr.Find("today").SetActive(false);
        tr.Find("img_icon").GetComponent<Image>().color = new Color(.2f, .2f, .2f);
        tr.Find("img_check").SetActive(true);
        tr.Find("txt_count").GetText().color = Color.gray;
        tr.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    void RefreshToday()
    {
        // var tr = tr_table.GetChild((User.data.GetField<int>(key)) % CYCLE);
        // var check = User.data.GetField<bool>(keyCheck);
        // tr.Find("today").SetActive(!check);
        // tr.GetComponent<Button>().onClick.AddListener(GetReward);
        // if (check) SetPassed(tr);
        // NotiBadges.Noti(key, !check);
    }


    public void GetReward()
    {
        // if (User.data.GetField<bool>(keyCheck)) return;
        // User.data.SetField<bool>(keyCheck, true);

        // var reward = GameData.meta.GetField<Dictionary<int, Resource>>
        //     (key)[(User.data.GetField<int>(key)) % CYCLE];
        // RefreshToday();
        // User.inst.GetReward(reward, title.L());
    }
}
