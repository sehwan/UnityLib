using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;



public class QuestItem : MonoBehaviour
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
    public GameObject go_completed;


    public void Init(QuestMeta meta)
    {
        this.meta = meta;
        // icon_reward.sprite = UIUtil.GetIcon(meta.t);
        icon_reward.sprite = meta.t.GetIcon();
        txt_rcount.text = meta.n.ToString();
        gameObject.SetActive(true);
        Refresh();
    }

    public void Refresh()
    {
        RefreshAble();
        // var users = User.data.dq.GetValueOrNew(meta.id);
        // var req = meta.req;
        // txt_title.text = $"q_{meta.id}".L();
        // txt_progress.FractionalText(users.v, req);
        // img_progress.FillAmount(users.v, req);
        // btn_reward.interactable = isAble;
        // go_completed.SetActive(users.l.ToBool());
    }

    public void RefreshAble()
    {
        // var users = User.data.dq.GetValueOrNew(meta.id);
        // bool isEnough = users.v >= meta.req;
        // isAble = isEnough && users.l.ToBool() == false;
        // if (isAble) NotiBadges.Noti("quest", true);
    }


    public void GetReward()
    {
        if (meta == null) return;

        // User.data.dq.GetValueOrNew(meta.id).l = true.ToInt();
        // User.inst.GetReward(meta, $"q_{meta.id}".L());

        Refresh();
        UM.Get<QuestsPanel>().CheckNoti();
        // FirebaseMng.Log($"dq_{meta.id}");
    }
}
