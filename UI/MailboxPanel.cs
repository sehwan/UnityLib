using System.Linq;
using System.Collections.Generic;
using UnityEngine;
// using Firebase.Database;
// using Firebase.Extensions;


public class MailboxPanel : UIWindow
{
    public Transform tr_contents;
    public Dictionary<string, DataMail> mails = new();
    GameObject prefab;



    public override void Init()
    {
        prefab = Resources.Load("Prefabs/mail_item") as GameObject;
        // FirebaseDatabase.DefaultInstance.GetReference($"mails/{FirebaseMng.inst.user.UserId}")
        //     .GetValueAsync().ContinueWithOnMainThread(task =>
        //     {
        //         mails = task.Result.GetRawJsonValue().ToObject<Dictionary<string, DataMail>>();
        //         CheckAlarm();
        //     });
        // foreach (Transform c in tr_contents) Destroy(c.gameObject);
    }


    public override void Show()
    {
        base.Show();
        // if (mails == null || mails.Count == 0) return;
        tr_contents.Pool(prefab, mails.Count);
        for (int i = 0; i < mails.Count; i++)
        {
            var mail = tr_contents.GetChild(i).GetComponent<MailItem>();
            mail.Init(mails.ElementAt(mails.Count - i - 1));
        }
    }


    public void CheckAlarm()
    {
        NotiBadges.Noti("btn_mailbox", mails.Count);
    }
}
