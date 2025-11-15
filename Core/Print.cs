using UnityEngine;

static public class Print
{
    static public bool isVerbose = true;


    static public void Gray(object message)
    {
        if (isVerbose == false) return;
        Debug.Log($"<color=grey>{message}</color>");
    }
    static public void Log(object message)
    {
        Debug.Log(message);
    }
    static public void Warn(object message)
    {
        Debug.Log($"<color=yellow>{message}</color>");
    }
}