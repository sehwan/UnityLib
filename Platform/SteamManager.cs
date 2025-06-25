using UnityEngine;
using Steamworks;

public class SteamManager : MonoBehaviour
{
    public static SteamManager i;
    public uint steamAppId = 3457200;
    // public uint steamAppId = 480;
    public bool isSteamInitialized = false;


    void Awake()
    {
        if (i == null)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        try
        {
            SteamClient.Init(steamAppId);
            isSteamInitialized = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to initialize Steamworks: " + e.Message);
            isSteamInitialized = false;
            throw;
        }

    }

    public void OnDestroy()
    {
        // Steam won't actually show that you've stopped playing the game at this point. 
        // It doesn't do that until the exe and any child processes are closed. It sucks, but that's the way it is.
        // This also means that in the Unity Editor it'll show as in game until you close the editor, 
        // but subsequent SteamClient.Init calls are needed and will work.
        if (isSteamInitialized)
        {
            SteamClient.Shutdown();
            isSteamInitialized = false;
        }
    }

    void Start()
    {
        Debug.Log($"Player Name: {SteamClient.Name}");
        Debug.Log($"Player Steam ID: {SteamClient.SteamId}");
        Debug.Log($"<color=green>{SteamApps.AppOwner}</color>");
        Debug.Log($"<color=green>{SteamApps.AvailableLanguages}</color>");
        Debug.Log($"<color=green>{SteamApps.BuildId}</color>");
        Debug.Log($"<color=green>{SteamApps.GameLanguage}</color>");
        foreach (var controller in SteamInput.Controllers)
            Debug.Log($"<color=green>{controller.Id}</color>");

        SteamScreenshots.TriggerScreenshot();

        // SteamUserStats.SetStat("deaths", value);

        // foreach (var item in SteamInventory.Items)
        // {
        //     Debug.Log($"{item.Def.Name} x {item.Quantity}");
        // }

        foreach (var player in SteamFriends.GetFriends())
            Debug.Log($"{player.Name}");
    }

    void Update()
    {
        if (isSteamInitialized == false) return;
        SteamClient.RunCallbacks();
    }

    void SetHighscore(string key, int value)
    {
        if (isSteamInitialized == false) return;
        var oldTotal = SteamUserStats.GetStatInt(key);
        if (oldTotal >= value) return;
        SteamUserStats.SetStat(key, value);
        SteamUserStats.StoreStats();
    }

}
