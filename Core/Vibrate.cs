using UnityEngine;

public static class Vibration
{
    static bool isLoaded;
    static bool _noVibrate;
    public static bool NoVibrate
    {
        get
        {
            if (isLoaded == false)
            {
                isLoaded = true;
                _noVibrate = PlayerPrefs.GetInt("key_NoVibrate", 0).ToBool();
            }
            return _noVibrate;
        }
        set
        {
            _noVibrate = value;
            PlayerPrefs.SetInt("key_NoVibrate", value.ToInt());
        }
    }


#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    static void NoUseButNeedForAutoPermission() { Handheld.Vibrate(); }
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif


    public static void Vibrate()
    {
        if (NoVibrate) return;
#if UNITY_ANDROID && !UNITY_EDITOR
        vibrator.Call("vibrate");
#elif UNITY_IOS
        Handheld.Vibrate();
#endif
    }

    public static void Vibrate(long milliseconds)
    {
        if (NoVibrate) return;
#if UNITY_ANDROID && !UNITY_EDITOR
        vibrator.Call("vibrate", milliseconds);
#elif UNITY_IOS
        Handheld.Vibrate();
#endif
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        if (NoVibrate) return;
#if UNITY_ANDROID && !UNITY_EDITOR
        vibrator.Call("vibrate", pattern, repeat);
#elif UNITY_IOS
        Handheld.Vibrate();
#endif
    }

    public static void Cancel()
    {
        if (NoVibrate) return;
#if UNITY_ANDROID && !UNITY_EDITOR
        vibrator.Call("cancel");
#endif
    }
}