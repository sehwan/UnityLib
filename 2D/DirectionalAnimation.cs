using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

/*  
Usage
Load(CharName), SetDirection(u,d,l,r), Loop
file ex) Characters/CharName/u_idle_0_5
*/


[System.Serializable]
public class DataFrame
{
    public Sprite spr;
    public float dur;
}

public enum PlayMode { Once, Loop, Pong }


public class DirectionalAnimation : MonoBehaviour
{
    static SerializedDictionary<string, SerializedDictionary<string, DataFrame>> all = new();

    [Header("Settings")]
    public SpriteRenderer ren;
    public string dirPath;
    public float speed = 1;


    [Header("State")]
    public SerializedDictionary<string, DataFrame> dic;
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
        if (all.ContainsKey(unit))
        {
            dic = all[unit];
            return;
        }

        var sprites = Resources.LoadAll<Sprite>(unit);
        foreach (var file in sprites)
        {
            var splitted = file.name.Split('_');
            if (splitted.Length < 4) continue;
            var dir = splitted[0];
            var key = splitted[1];
            var idx = splitted[2];
            var dur = splitted[3];
            var frameName = $"{dir}_{key}_{idx}";
            var frame = new DataFrame
            {
                spr = file,
                dur = float.Parse(dur) * 0.1f
            };
            if (all.ContainsKey(unit) == false)
                all.Add(unit, new SerializedDictionary<string, DataFrame>());
            all[unit][frameName] = frame;
        }
        dic = all[unit];
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
            sum += dic[$"{curDir}_{curName}_{idx}"].dur;
        }
        return sum;
    }

    DataFrame CurFrame()
    {
        var cur = $"{curDir}_{curName}_{curFrame}";
        if (dic.ContainsKey(cur) == false) return null;
        return dic[cur];
    }
    DataFrame NextFrame()
    {
        var next = $"{curDir}_{curName}_{curFrame + 1}";
        if (dic.ContainsKey(next) == false) return null;
        return dic[next];
    }


    public void Once(string key)
    {
        StopAllCoroutines();
        StartCoroutine(Co_Once(key));
    }
    public void Loop(string key)
    {
        if (key.Equals(curName)) return;
        if (dic.ContainsKey($"{curDir}_{key}_0") == false) return;
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
            yield return CoroutineEx.GetWait(frame.dur);
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
            yield return CoroutineEx.GetWait(frame.dur);
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
            yield return CoroutineEx.GetWait(frame.dur);
            if (isRewinding) curFrame--;
            else curFrame++;
        }
    }
}
