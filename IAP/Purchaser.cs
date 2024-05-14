// // https://docs.unity3d.com/Packages/com.unity.purchasing@4.1/manual/UnityIAPInitialization.html
// // https://algorfati.tistory.com/161?category=872581
// using System;
// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using UnityEngine;
// using Unity.Services.Core;
// using Unity.Services.Core.Environments;
// using UnityEngine.Purchasing;
// using UnityEngine.Purchasing.Extension;

// public class Purchaser : MonoSingleton<Purchaser>, IDetailedStoreListener
// {
//     public static IStoreController controller; // The Unity Purchasing system.
//     private static IExtensionProvider extensions; // The store-specific Purchasing subsystems.
//     public string ENV = "production";


//     public async override void Init()
//     {
//         base.Init();
//         // UGS
//         try
//         {
//             var options = new InitializationOptions().SetEnvironmentName(ENV);
//             await UnityServices.InitializeAsync(options);
//         }
//         catch (Exception exception)
//         {
//             Debug.LogException(exception);
//         }
//         // IAP
//         var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//         foreach (var item in GameData.meta.iap)
//         {
//             if (item.Value.priceType == "cash")
//                 builder.AddProduct(item.Key, ProductType.Consumable);
//         }
//         UnityPurchasing.Initialize(this, builder);
//     }

//     public bool IsInitialized() => controller != null && extensions != null;

//     public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//     {
//         Purchaser.controller = controller;
//         Purchaser.extensions = extensions;
//     }
//     public void OnInitializeFailed(InitializationFailureReason error)
//     {
//         ToastGroup.Alert("InitializationFailureReason:" + error);
//     }
//     public void OnInitializeFailed(InitializationFailureReason error, string message)
//     {
//         ToastGroup.Alert($"OnInitializeFailed InitializationFailureReason:{error}, message:{message}");
//     }


//     public void Buy(string productId)
//     {
//         if (IsInitialized())
//         {
//             var product = controller.products.WithID(productId);
//             if (product != null && product.availableToPurchase)
//             {
//                 Debug.Log($"Purchasing product asychronously: '{product.definition.id}'");
//                 controller.InitiatePurchase(product);
//             }
//             else ToastGroup.Alert("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//         }
//         else ToastGroup.Alert("BuyProductID FAIL. Not initialized.");
//     }

//     public void RestorePurchases()
//     {
//         if (!IsInitialized())
//         {
//             ToastGroup.Show("RestorePurchases FAIL. Not initialized.");
//             return;
//         }

//         if (Application.platform == RuntimePlatform.IPhonePlayer ||
//             Application.platform == RuntimePlatform.OSXPlayer)
//         {
//             if (Application.isEditor)
//                 ToastGroup.Show("RestorePurchases started ...");

//             var apple = extensions.GetExtension<IAppleExtensions>();
//             apple.RestoreTransactions((result, message) =>
//             {
//                 if (Application.isEditor)
//                     ToastGroup.Show("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//             });
//         }
//         else
//         {
//             ToastGroup.Show("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//         }
//     }

//     // Server-Side Validation
//     public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
//     {
//         Product purchased = e.purchasedProduct;
//         NetworkMng.inst.ValidateReceipt(purchased.receipt, () =>
//         {
//             // Logic
//             GameData.meta.iap[purchased.definition.id].GetPurchased();
//             controller.ConfirmPendingPurchase(e.purchasedProduct);
//         });
//         return PurchaseProcessingResult.Complete;
//     }

//     public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureReason)
//     {
//         if (failureReason.reason == PurchaseFailureReason.UserCancelled) return;
//         ToastGroup.Alert($"PurchaseFailureReason: {failureReason}");
//         FirebaseMng.Log("purchase_failed", "reason", failureReason.ToString());
//     }
//     public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//     {
//         if (failureReason == PurchaseFailureReason.UserCancelled) return;
//         ToastGroup.Alert($"PurchaseFailureReason: {failureReason.ToString()}");
//         FirebaseMng.Log("purchase_failed", "reason", failureReason.ToString());
//     }
// }