using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
// using Firebase.Database;

public class MailItem : MonoBehaviour
{
    public KeyValuePair<string, DataMail> data;

    [Header("Ref")]
    public Image img_icon;
    public Text txt_count;
    public Text txt_title;
    public Text txt_detail;



    public void Init(KeyValuePair<string, DataMail> d)
    {
        gameObject.SetActive(true);
        data = d;
        txt_title.text = d.Value.t.L();
        txt_detail.text = $"{d.Value.r.L()} x{d.Value.n:n0}";
        img_icon.sprite = UIUtil.GetIcon(d.Value.r);
        txt_count.text = d.Value.n > 0 ? $"x{d.Value.n:n0}" : "";
    }

    public void OnClick()
    {
        // Remove
        // FirebaseDatabase.DefaultInstance
        //     .GetReference($"mails/{FirebaseMng.inst.user.UserId}/{data.Key}")
        //     .RemoveValueAsync();
        var box = UM.Get<MailboxPanel>();
        box.mails.Remove(data.Key);
        box.CheckAlarm();
        NotiBadges.Noti("btn_mailbox", box.mails.Count);
        // User.inst.RecordInt("mail");
        // User.inst.GetReward(data.Value, "Mail".L());
        // if (data.Value.n == 0) FirebaseMng.LogE("EmptyMail");
        Destroy(gameObject);
    }
}
