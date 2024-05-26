using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestRepeatingItem : MonoBehaviour
{
    QuestMeta meta;
    public bool isAble;

    [Header("UI")]
    public Text txt_title;
    public Image img_progress;
    public Text txt_progress;
    public Image icon_reward;
    public Text txt_rcount;
    public Button btn_reward;


    public void Init(QuestMeta meta)
    {
        this.meta = meta;
        gameObject.SetActive(true);
        icon_reward.sprite = UIUtil.GetIcon(meta.r);
        txt_rcount.text = meta.n.ToString();
        Refresh();
    }

    public void Refresh()
    {
        RefreshAble();
        var users = User.i._.rq.GetValueOrNew(meta.id);
        txt_title.text = $"q_{meta.id}".L() + $" Lv.{users.l + 1}";
        var req = meta.req * (users.l + 1);
        txt_progress.FractionalText(users.v, req);
        img_progress.FillAmount(users.v, req);
        btn_reward.interactable = isAble;
    }

    public void RefreshAble()
    {
        var users = User.i._.rq.GetValueOrNew(meta.id);
        var req = meta.req * (users.l + 1);
        isAble = users.v >= req;
        if (isAble) NotiBadges.Noti("quest", true);
    }


    public void GetReward()
    {
        if (meta == null) return;

        User.i._.rq.GetValueOrNew(meta.id).l++;
        User.i.GetReward(meta, $"q_{meta.id}".L());

        Refresh();
        UM.Get<QuestsPanel>().CheckNoti();
        // FirebaseMng.Log($"rq_{meta.id}");
    }
}
