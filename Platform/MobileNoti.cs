// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Unity.Notifications.Android;
// // https://docs.unity3d.com/Packages/com.unity.mobile.notifications@1.4/manual/Android.html


// public class DataNoti
// {
//     public string ch, title, text;
//     public int delay;
//     public DataNoti(string ch, string text, int delay)
//     {
//         this.ch = ch;
//         this.text = text;
//         this.delay = delay;
//     }
// }

// // Use "RegisterNotiMaker()" rather than Register() straight away.
// public class MobileNoti : MonoSingleton<MobileNoti>
// {
//     public static List<string> chs = new List<string>();
//     public static Dictionary<string, int> ids = new Dictionary<string, int>();

//     public void Register(string ch, string text, int delay)
//     {
// #if UNITY_ANDROID
//         // Channel
//         if (chs.Contains(ch) == false)
//         {
//             AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel()
//             {
//                 Id = ch,
//                 Name = ch,
//                 Importance = Importance.Default,
//                 Description = ch,
//             });
//             chs.Add(ch);
//         }

//         // Noti
//         var nick = User.data.nick == UserData.DefaultNick ? null : User.data.nick;
//         var noti = new AndroidNotification();
//         noti.Title = "dear{0}".LF(nick);
//         noti.Text = text;
//         var target = System.DateTime.Now.AddSeconds(delay);
//         DateTime fin = target;
//         if (target.Hour < 9)
//             fin = new DateTime(target.Year, target.Month, target.Day,
//                 9, 0, 0
//             );
//         else if (target.Hour >= 22)
//             fin = new DateTime(target.Year, target.Month, target.Day,
//                 9, 0, 0
//             ).AddDays(1);
//         noti.FireTime = fin;
//         // Register
//         if (ids.ContainsKey(ch)) AndroidNotificationCenter.CancelNotification(ids[ch]);
//         ids[ch] = AndroidNotificationCenter.SendNotification(noti, ch);

//         Debug.Log($"noti {text}/{fin}");
// #endif
//     }
//     void OnDestroy()
//     {
//         notiMakers.Clear();
//     }


// #if UNITY_ANDROID
//     void OnApplicationFocus(bool hasFocus)
//     {
//         if (hasFocus == false) return;
//         AndroidNotificationCenter.CancelAllScheduledNotifications();
//     }
// #endif


//     List<Func<DataNoti>> notiMakers = new List<Func<DataNoti>>();
//     public void RegisterNotiMaker(Func<DataNoti> func)
//     {
//         if (func == null) return;
//         notiMakers.Add(func);
//     }

// #if UNITY_ANDROID
//     void OnApplicationQuit()
//     {
//         WhenOff();
//     }
//     void OnApplicationPause(bool paused)
//     {
//         if (paused == false) return;
//         WhenOff();
//     }
// #endif

//     public void WhenOff()
//     {
//         Register("full", "noti_FullOfftime".L(), 60 * 60 * 6);
//         Register("missing", "noti_MissingYou".L(), 60 * 60 * 24 * 1 - 1800);
//         Register("missing", "noti_MissingYou".L(), 60 * 60 * 24 * 2 - 1800);
//         Register("missing", "noti_MissingYou".L(), 60 * 60 * 24 * 7 - 1800);
//         foreach (var e in notiMakers)
//         {
//             var noti = e();
//             if (noti == null) continue;
//             Register(noti.ch, noti.text, noti.delay);
//         }
//     }
// }