using UnityEngine;
using System;

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



    // using 패턴으로 스택 트레이스 자동 복원
    static public IDisposable HideTrace()
    {
        return new TraceDisabler();
    }

    class TraceDisabler : IDisposable
    {
        public TraceDisabler()
        {
            SetShowTrace(false);
        }

        public void Dispose()
        {
            SetShowTrace(true);
        }
    }
    static public void SetShowTrace(bool show)
    {
        Application.SetStackTraceLogType(LogType.Log, show ? StackTraceLogType.ScriptOnly : StackTraceLogType.None);
        Application.SetStackTraceLogType(LogType.Warning, show ? StackTraceLogType.ScriptOnly : StackTraceLogType.None);
        Application.SetStackTraceLogType(LogType.Error, show ? StackTraceLogType.ScriptOnly : StackTraceLogType.None);
    }
}