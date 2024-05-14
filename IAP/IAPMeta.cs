// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// [System.Serializable]
// public class IAPMeta
// {
//     public string id;
//     public string category;
//     public bool isOn;
//     public int hot;
//     public int limit;
//     public bool life, week, month, day;
//     public string phrase;

//     public int originalPrice;
//     public string priceType;
//     public int price;

//     // Rewards
//     public int duration;
//     public int gem, gold, soul, rbox, ticket, tk_ad;
//     public string feature;
//     public string pass;

//     public static string KEY_CASH = "cash";
//     public static string KEY_ADS = "ads";
//     public static string KEY_TIME = "time";


//     public bool IsLimitedAndPurchased()
//     {
//         if (limit <= 0) return false;
//         if (User.data.rcd_int.ContainsKey(id) == false) return false;
//         if (User.data.rcd_int[id] < limit) return false;
//         return true;
//     }
//     public bool IsByTimeAndPurchased()
//     {
//         return User.data.dts_iap.ContainsKey(id) &&
//             (User.data.dts_iap[id] - DateTime.Now).TotalSeconds > 1;
//     }

//     public void PurchaseByCash(Action cb = null)
//     {
//         callback = cb;
//         if (Application.isEditor)
//             GetPurchased();
//         else Purchaser.inst.Buy(id);
//     }
//     Action callback;
//     public void GetPurchased()
//     {
//         var userData = User.data;

//         // Reset Duration
//         if (duration != 0)
//         {
//             if (userData.dts_iap.ContainsKey(id) == false) userData.dts_iap.Add(id, DateTime.Now);
//             //it would be 0000-00-00, it should be more than Now
//             if (userData.dts_iap[id] < DateTime.Now)
//                 userData.dts_iap[id] = DateTime.Now;
//             if (duration == -1)
//                 userData.dts_iap[id] = DateTime.MaxValue;
//             userData.dts_iap[id] = userData.dts_iap[id].AddDays(duration);
//         }
//         // Bonus & VIP
//         if (priceType == KEY_CASH)
//         {
//             // RewardMsg.inst.Enqueue("Bonus!".L(), "rbox", price);
//             // User.inst.AddResource("rbox", price);
//             userData.vip_exp += price;
//         }
//         // Record Limited Items
//         if (limit > 0) User.inst.RecordInt(id);

//         // Pass
//         if (pass.IsFilled()) userData.pass[pass].a = true;

//         GetReward();
//         User.inst.SaveAfterFrame();
//         ToastGroup.Show("Purchased".L());
//         // FirebaseMng.Log(id);
//         callback?.Invoke();
//     }

//     public void GetReward()
//     {
//         var user = User.inst;
//         if (gem > 0) user.GetReward(Resource.Gem(gem), id.L());
//     }
// }