using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : MonoSingleton<TimeManager>
{
    #region Loop
    static Dictionary<float, (Coroutine c, List<Action> a)> timers = new();
    public void AddTimer(float sec, Action cb)
    {
        if (timers.ContainsKey(sec) == false)
        {
            var list = new List<Action>() { cb };
            var co = StartCoroutine(Co_Timer(CoroutineEx.GetWait(sec), list));
            timers.Add(sec, (co, list));
        }
        else timers[sec].a.Add(cb);
    }
    IEnumerator Co_Timer(WaitForSeconds ws, List<Action> cbs)
    {
        while (true)
        {
            yield return ws;
            foreach (var cb in cbs) cb.Invoke();
        }
    }

    Dictionary<float, (Coroutine c, List<Action> a)> timersReal = new();
    public void AddTimerReal(float sec, Action cb)
    {
        print(sec);
        if (timersReal.ContainsKey(sec) == false)
        {
            var list = new List<Action>() { cb };
            var co = StartCoroutine(Co_TimerReal(CoroutineEx.GetReal(sec), list));
            timersReal.Add(sec, (co, list));
        }
        else timersReal[sec].a.Add(cb);
    }
    IEnumerator Co_TimerReal(WaitForSecondsRealtime ws, List<Action> cbs)
    {
        while (true)
        {
            yield return ws;
            foreach (var cb in cbs) cb.Invoke();
        }
    }
    #endregion

    #region TimeSclae
    public SerializedDictionary<GameObject, float> timeScaler = new();
    public void AddTimeScaler(GameObject go, float time)
    {
        if (timeScaler.ContainsKey(go)) timeScaler.Remove(go);
        timeScaler.Add(go, time);
        RefreshTimescale();
    }
    public void RemoveTimeScaler(GameObject go)
    {
        timeScaler.Remove(go);
        RefreshTimescale();
    }
    void RefreshTimescale()
    {
        var time = 1f;
        foreach (var kv in timeScaler) time *= kv.Value;
        Time.timeScale = time;
    }
    public void ResetTimeSlower()
    {
        timeScaler.Clear();
        Time.timeScale = 1;
    }
    #endregion
}