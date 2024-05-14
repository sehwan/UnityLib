// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using GoogleMobileAds.Api;
// public enum AdType { Interstitial, Reward }

// public class AdmobMng : MonoSingleton<AdmobMng>
// {
//     readonly string key_reward = "ca-app-pub-5204152166536557/1333146745";
//     readonly string key_banner = "ca-app-pub-5204152166536557/5712710611";
//     readonly string key_inter = "ca-app-pub-5204152166536557/1050305630";


//     Action cb;
//     bool finishWatching;


//     [Header("Settings")]
//     public int COOL_CHARGE = 100;
//     public int MAX_CHANCE = 30;

//     RewardedAd ad_reward;
//     RewardedInterstitialAd ad_inter;
//     BannerView ad_banner;


//     public override void Init()
//     {
//         base.Init();
//         MobileAds.SetiOSAppPauseOnBackground(true);
//         MobileAds.RaiseAdEventsOnUnityMainThread = true;
//         MobileAds.Initialize(initStatus => { });
//         LoadRewarded();
//         // LoadInter();
//         BannerView();
//     }
    
//     public void BannerView()
//     {
//         if (ad_banner != null)
//         {
//             ad_banner.Destroy();
//             ad_banner = null;
//         }
//         ad_banner = new BannerView(key_banner, AdSize.Banner, AdPosition.Bottom);
//         ad_banner.LoadAd(new AdRequest());
//     }

//     void LoadRewarded()
//     {
//         if (ad_reward != null)
//         {
//             ad_reward.Destroy();
//             ad_reward = null;
//         }
//         RewardedAd.Load(key_reward, new AdRequest(), (RewardedAd ad, LoadAdError err) =>
//         {
//             if (err != null || ad == null)
//             {
//                 Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + err);
//                 return;
//             }
//             ad.OnAdPaid += (AdValue adValue) =>
//             {
//                 finishWatching = true;
//             };
//             ad_reward = ad;
//         });
//     }
//     void LoadInter()
//     {
//         if (ad_inter != null)
//         {
//             ad_inter.Destroy();
//             ad_inter = null;
//         }
//         RewardedInterstitialAd.Load(key_inter, new AdRequest(), (RewardedInterstitialAd ad, LoadAdError err) =>
//         {
//             if (err != null || ad == null)
//             {
//                 Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + err);
//                 return;
//             }
//             ad.OnAdPaid += (AdValue adValue) =>
//             {
//                 finishWatching = true;
//             };
//             ad_inter = ad;
//         });
//     }

//     public void ShowRewarded(Action cb)
//     {
//         this.cb = cb;
//         finishWatching = false;
//         if (ad_reward == null || ad_reward.CanShowAd() == false)
//         {
//             ToastGroup.Alert("Ads is not Loaded".L());
//             // AdsButton.remain = AdsButton.COOLTIME * 2;
//             // cb();
//             LoadRewarded();
//         }
//         ad_reward.Show(reward =>
//         {
//             // finishWatching = true;
//             cb();
//             LoadRewarded();
//         });
//     }
//     public void ShowInter(Action cb)
//     {
//         this.cb = cb;
//         finishWatching = false;
//         if (ad_inter == null || ad_inter.CanShowAd() == false)
//         {
//             ToastGroup.Alert("Ads is not Loaded".L());
//             // AdsButton.remain = AdsButton.COOLTIME * 2;
//             cb();
//         }
//         ad_inter.Show(reward =>
//         {
//             // finishWatching = true;
//             cb();
//         });
//         LoadInter();
//     }

//     // void OnApplicationFocus(bool focus)
//     // {
//     //     if (focus == false) return;
//     //     if (finishWatching) cb();
//     //     finishWatching = false;
//     // }
// }
