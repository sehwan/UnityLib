// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Unity.Services.Authentication;
// using Unity.Services.Core;
// using Unity.Services.Vivox;
// using VivoxUnity;
// using System.Threading.Tasks;
// using Unity.Services.CloudSave;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using GooglePlayGames;
// using GooglePlayGames.BasicApi;

// public class UnityServicesMng : MonoBehaviour
// {
//     public static UnityServicesMng inst;
//     [Immutable] public bool wasSigned;

//     void Awake()
//     {
//         inst = this;
//     }
//     async Task Start()
//     {
//         // Binding
//         AuthenticationService.Instance.SignedIn += () =>
//         {
//             var playerId = AuthenticationService.Instance.PlayerId;
//             Debug.Log("Signed in as: " + playerId);
//             SaveToUnityServer();
//         };
//         AuthenticationService.Instance.SignInFailed += s =>
//         {
//             Debug.LogError(s);
//         };

//         await UnityServices.InitializeAsync();
//         // Login Unity Services
//         await AuthenticationService.Instance.SignInAnonymouslyAsync();
//         // while (Social.localUser.authenticated == false) await Task.Delay(100);
//         // string googleIDToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
//         // Debug.Log($"<color=cyan>id {googleIDToken}</color>");
//         // await AuthenticationService.Instance.SignInWithGoogleAsync(googleIDToken);

//         VivoxService.Instance.Initialize();
//         await LoadUserDataFromUnityServer();
//         wasSigned = true;
//     }


//     async void SaveToUnityServer()
//     {
//         var data = new Dictionary<string, object>();
//         data.Add("a", "a");
//         data.Add("b", 12);
//         data.Add("c", new DataSStone());
//         await SaveData.ForceSaveAsync(data);
//     }


//     async Task<DataLogin> LoadUserDataFromUnityServer()
//     {
//         var savedData = await SaveData.LoadAllAsync();
//         foreach (var item in savedData)
//         {
//             Debug.Log($"<color=cyan>{item.Key} {item.Value}</color>");
//         }
//         //dictionary to class
//         var login = new DataLogin();
//         login.user = JsonConvert.DeserializeObject<DataUser>(savedData["data"].ToString());
//         return login;
//     }
// }
