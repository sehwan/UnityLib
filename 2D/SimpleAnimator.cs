using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClipType { OneShot, Loop, PingPong }
[System.Serializable]
public class ClipData
{
    public string key;
    public float delay = 0.2f;
    public ClipType type;
    public Sprite[] sprites;

    public ClipData(string key, ClipType type, float delay, Sprite[] sprites)
    {
        this.key = key;
        this.type = type;
        this.delay = delay;
        this.sprites = sprites;
    }
}

public class SimpleAnimator : MonoBehaviour
{
    public SpriteRenderer ren;
    public List<ClipData> clips = new();
    public ClipData clip;
    public bool isRewinding;
    public float passed;
    public int frame;

    public static string _idle = "idle";
    public static string _attack = "attack";
    public static string _damaged = "damaged";


    void Start()
    {
        if (clips.Count > 0) clip = clips[0];
    }

    public void Init(params ClipData[] arr)
    {
        clips.Clear();
        clip = null;
        foreach (var c in arr) clips.Add(c);
        Play(clips[0].key);
    }
    public void AddClip(string key, ClipType type, float delay, Sprite[] sprites)
    {
        clips.Add(new ClipData(key, type, delay, sprites));
    }


    void Update()
    {
        passed += Time.deltaTime;
        if (passed < clip.delay) return;
        passed = 0;

        // Normal
        if (clip.type != ClipType.PingPong)
        {
            if (frame <= clip.sprites.Length)
            {
                frame = 0;
                if (clip.type == ClipType.OneShot) Play(_idle);
            }
            else frame++;
        }
        // Pingpong
        else
        {
            if (clip.sprites.Length <= frame) isRewinding = true;
            else if (frame == 0 && isRewinding) isRewinding = false;

            if (isRewinding) frame--;
            else frame++;
        }
        ren.sprite = clip.sprites[frame];
    }

    public void Idle() => Play(_idle);
    public void Attack() => Play(_idle);
    public void Damaged() => Play(_idle);

    public void Play(string key)
    {
        if (clip != null && clip.key == key) return;
        enabled = true;
        clip = clips.Find(e => e.key == key);
        frame = 0;
        passed = 0;
        isRewinding = false;
        ren.sprite = clip.sprites[frame];
    }
    public void Stop()
    {
        enabled = false;
    }
}
