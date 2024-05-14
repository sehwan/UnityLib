using UnityEngine;

public static class Settings
{
    // Options
    public static int FrameRate
    {
        get { return PlayerPrefs.GetInt("framerate", 60); }
        set { PlayerPrefs.SetInt("framerate", value); }
    }
    public static bool NoPushnoti
    {
        get { return PlayerPrefs.GetInt("key_NoPushnoti", 0).ToBool(); }
        set { PlayerPrefs.SetInt("key_NoPushnoti", value.ToInt()); }
    }
    public static bool NoScreenSaver
    {
        get { return PlayerPrefs.GetInt("key_NoScreenSaver", 0).ToBool(); }
        set { PlayerPrefs.SetInt("key_NoScreenSaver", value.ToInt()); }
    }
    public static bool MuteBGM
    {
        get { return PlayerPrefs.GetInt("key_mute_bgm", 0).ToBool(); }
        set { PlayerPrefs.SetInt("key_mute_bgm", value.ToInt()); }
    }
    public static bool MuteSFX
    {
        get { return PlayerPrefs.GetInt("key_mute_sfx", 0).ToBool(); }
        set { PlayerPrefs.SetInt("key_mute_sfx", value.ToInt()); }
    }
    public static float VolumeSFX
    {
        get { return PlayerPrefs.GetFloat("key_volume_sfx", .8f); }
        set { PlayerPrefs.SetFloat("key_volume_sfx", value); }
    }
    public static float VolumeBGM
    {
        get { return PlayerPrefs.GetFloat("key_volume_bgm", 0.8f); }
        set { PlayerPrefs.SetFloat("key_volume_bgm", value); }
    }
}