using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public struct DataRewardMsg
{
    public string msg;
    public string type;
    public int count;
    public BigNumber count_big;
}


public class RewardMsg : PrefabSignleton<RewardMsg>
{
    public static RewardMsg inst
    {
        get
        {
            prefabPath = "Prefabs/RewardMsg";
            return instance;
        }
    }

    public GameObject frame;
    public Text txt;
    public Text txt_rsc;
    public Image img_rsc;
    public ItemSlot slot;
    public Button btn_esc, btn_ok, btn_dim;

    public List<DataRewardMsg> queue = new();


    public void Enqueue(string s, string t, int c)
    {
        gameObject.SetActive(true);
        DataRewardMsg msg = new();
        msg.msg = s;
        msg.type = t;
        msg.count = c;
        msg.count_big = 0;
        queue.Add(msg);
        if (queue.Count == 1) Show();
    }
    public void Enqueue(string s, string t, BigNumber c)
    {
        gameObject.SetActive(true);
        DataRewardMsg msg = new();
        msg.msg = s;
        msg.type = t;
        msg.count = 0;
        msg.count_big = c;
        queue.Add(msg);
        if (queue.Count == 1) Show();
    }
    public void Enqueue(string s, Resource r)
    {
        gameObject.SetActive(true);
        var msg = new DataRewardMsg();
        msg.msg = s;
        msg.type = r.r;
        msg.count = r.n;
        msg.count_big = r.b != null ? r.b : new BigNumber(0);
        queue.Add(msg);
        if (queue.Count == 1) Show();
    }

    void Show()
    {
        frame.SetActive(false);
        frame.SetActive(true);

        var cur = queue[0];
        txt.text = cur.msg;
        if (cur.count_big != 0)
        {
            img_rsc.SetActive(true);
            img_rsc.sprite = UIUtil.GetIcon(cur.type);
            txt_rsc.SetActive(true);
            txt_rsc.text = cur.count_big.ToString();
        }
        else if (cur.count != 0)
        {
            img_rsc.SetActive(true);
            img_rsc.sprite = UIUtil.GetIcon(cur.type);
            txt_rsc.SetActive(true);
            txt_rsc.text = cur.count.ToString("n0");
        }
        else
        {
            img_rsc.SetActive(false);
            txt_rsc.SetActive(false);
        }
    }

    public void ESC()
    {
        queue.RemoveAt(0);
        if (queue.Count > 0) Show();
        else gameObject.SetActive(false);
    }
}
