using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class DataAchievement
{
    public int val;
    public int lv;
}


public class AchievementItem : MonoBehaviour
{
    string type;
    public bool isAble;
    public bool isOver;

    [Header("UI")]
    public Text txt_title;
    public Image img_progress;
    public Text txt_progress;
    public Image icon_reward;
    public Text txt_rcount;
    public Button btn_reward;
    public GameObject go_complete;



    // public void Refresh(string t)
    // {
    //     type = t;
    //     DataAchievement users = User.data.achv.GetValueDefault(t);
    //     MetaAchievement meta = MetaAchievement.GetValue(type, users.lv);
    //     isAble = meta != null && users.val >= meta.req;
    //     btn_reward.interactable = isAble;
    //     txt_title.text = $"quest_{type}".Localize() + " Lv." + (users.lv + 1);

    //     if (meta != null)
    //     {
    //         txt_progress.FractionalText(users.val, meta.req);
    //         img_progress.FillAmount(users.val, meta.req);
    //         icon_reward.sprite = Util_UI.GetIcon(meta.reward);
    //         txt_rcount.text = meta.cnt.ToString();
    //         go_complete.SetActive(false);
    //     }
    //     // All Clear
    //     else
    //     {
    //         txt_progress.FractionalText(users.val, 0);
    //         img_progress.fillAmount = 0;
    //         go_complete.SetActive(true);
    //     }
    // }

    // public void CheckAlarm(string t)
    // {
    //     type = t;
    //     DataAchievement users = User.data.achv[type];
    //     MetaAchievement meta = MetaAchievement.GetValue(type, users.lv);
    //     isAble = meta != null && users.val >= meta.req;
    //     if (isAble) UM.Get<UIAchievements>().Refresh();
    // }


    // public void GetReward()
    // {
    //     if (type.IsNullOrEmpty()) return;
    //     SFXManager.inst.clip_coin.Play();

    //     DataAchievement users = User.data.achv[type];
    //     users.lv++;

    //     MetaAchievement meta = MetaAchievement.GetValue(type, users.lv);
    //     User.inst.GetReward(meta, "Finish_Achv".LocalizeFormat(type.Localize("_quest")));

    //     // Notify
    //     Refresh(type);
    //     var pn = UM.Get<UIAchievements>();
    //     var cnt_noti = pn.items.Values
    //         .Where(e => e.isAble)
    //         .Select(e => e).Count();
    //     NotiBadges.Noti("btn_achv", cnt_noti);
    //     // FirebaseMng.Log("reward_achievement", "key", table.key);
    // }
}
