using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class DataFrame
{
    public Sprite spr;
    public float dur;
}

public enum PlayMode { Once, Loop, Pong }

public class DirectionalAnimation : MonoBehaviour
{
    static Dictionary<string, Dictionary<string, DataFrame>> dic =
        new();

    [Header("Settings")]
    public SpriteRenderer ren;
    public string dirPath;
    public float speed = 1;


    [Header("State")]
    public PlayMode mode;
    [Immutable] public bool isRewinding;

    public string curName;
    public int curFrame;
    public string curDir;


    void Awake()
    {
        if (dirPath.IsFilled()) Load(dirPath);
    }
    public void Load(string unit)
    {
        dirPath = unit;
        if (dic.ContainsKey(unit)) return;

        var all = Resources.LoadAll<Sprite>(unit);
        foreach (var file in all)
        {
            var splitted = file.name.Split('_');
            if (splitted.Length < 4) continue;
            var dir = splitted[0];
            var key = splitted[1];
            var idx = splitted[2];
            var dur = splitted[3];
            var frameName = $"{dir}_{key}_{idx}";
            var frame = new DataFrame();
            frame.spr = file;
            frame.dur = float.Parse(dur) * 0.1f;
            if (dic.ContainsKey(unit) == false) dic.Add(unit, new Dictionary<string, DataFrame>());
            dic[unit][frameName] = frame;
        }
    }


    public void SetDirection(string dir)
    {
        curDir = dir;
        if (curName.IsFilled()) ren.sprite = CurFrame().spr;
    }
    public float GetClipTime()
    {
        var sum = 0f;
        var idx = 0;
        while (true)
        {
            if (NextFrame() == null) break;
            sum += dic[dirPath][$"{curDir}_{curName}_{idx}"].dur;
        }
        return sum;
    }

    DataFrame CurFrame()
    {
        var cur = $"{curDir}_{curName}_{curFrame}";
        if (dic[dirPath].ContainsKey(cur) == false) return null;
        return dic[dirPath][cur];
    }
    DataFrame NextFrame()
    {
        var next = $"{curDir}_{curName}_{curFrame + 1}";
        if (dic[dirPath].ContainsKey(next) == false) return null;
        return dic[dirPath][next];
    }


    public void Once(string key)
    {
        StopAllCoroutines();
        StartCoroutine(Co_Once(key));
    }
    public void Loop(string key)
    {
        if (key.Equals(curName)) return;
        if (dic[dirPath].ContainsKey($"{curDir}_{key}_0") == false) return;
        StopAllCoroutines();
        StartCoroutine(Co_Loop(key));
    }
    public void Pong(string key)
    {
        StopAllCoroutines();
        StartCoroutine(Co_Pong(key));
    }
    public void Stop()
    {
        StopAllCoroutines();
    }


    IEnumerator Co_Once(string key)
    {
        mode = PlayMode.Once;
        curName = key;
        curFrame = 0;
        while (true)
        {
            var frame = CurFrame();
            if (frame == null) break;
            ren.sprite = frame.spr;
            yield return new WaitForSeconds(frame.dur);
            curFrame++;
        }
        Loop("idle");
    }
    IEnumerator Co_Loop(string key)
    {
        mode = PlayMode.Loop;
        curName = key;
        curFrame = 0;
        while (true)
        {
            var frame = CurFrame();
            if (frame == null)
            {
                curFrame = 0;
                frame = CurFrame();
            }
            ren.sprite = frame.spr;
            yield return new WaitForSeconds(frame.dur);
            curFrame++;
        }
    }

    IEnumerator Co_Pong(string key)
    {
        mode = PlayMode.Pong;
        curName = key;
        curFrame = 0;
        isRewinding = false;
        while (true)
        {
            var frame = CurFrame();
            if (frame == null)
            {
                isRewinding = !isRewinding;
                if (isRewinding)
                {
                    curFrame -= 2;
                    frame = CurFrame();
                }
                else
                {
                    curFrame += 2;
                    frame = CurFrame();
                }
            }
            ren.sprite = frame.spr;
            yield return new WaitForSeconds(frame.dur);
            if (isRewinding) curFrame--;
            else curFrame++;
        }
    }
}
