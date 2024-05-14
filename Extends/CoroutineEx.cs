using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineEx
{
    public static Dictionary<int, WaitForSeconds> sec = new();
    public static Dictionary<int, WaitForSecondsRealtime> real = new();
    public static WaitForSeconds GetWait(float f)
    {
        int key = (int)System.Math.Round(f * 100);
        if (sec.ContainsKey(key) == false)
            sec.Add(key, new WaitForSeconds(f));
        return sec[key];
    }
    public static WaitForSecondsRealtime GetReal(float f)
    {
        int key = (int)System.Math.Round(f * 100);
        if (real.ContainsKey(key) == false)
            real.Add(key, new WaitForSecondsRealtime(f));
        return real[key];
    }
}
