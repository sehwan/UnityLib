using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataAnimationClip
{
    public string key;
    public string path;
    public int last;
    public float delay = 0.25f;
    public bool isRepeat;
    public bool isPingpong;
    public Sprite[] sprites;
    public DataAnimationClip Init()
    {
        sprites = new Sprite[last + 1];
        for (int i = 0; i <= last; i++)
        {
            sprites[i] = Resources.Load<Sprite>($"Animations/{path}_{i}");
        }
        return this;
    }
}

public class SimpleAnimator : MonoBehaviour
{
    public SpriteRenderer ren;
    public List<DataAnimationClip> list = new();
    public DataAnimationClip clip;
    public bool isRewinding;
    public bool playOnEnable;
    public float passed = 0.1f;
    public int frame;


    void Start()
    {
        list.ForEach(e => e.Init());
        if (list.Count > 0) clip = list[0];
        if (playOnEnable) Play(list[0].key);
    }
    public void AddClip(DataAnimationClip newClip)
    {
        list.Add(newClip.Init());
    }


    void Update()
    {
        passed += Time.deltaTime;
        if (passed < clip.delay) return;

        passed = 0;
        // normal
        if (clip.isPingpong == false)
        {
            if (clip.last <= frame) frame = 0;
            else frame++;
        }
        // pingpong
        else
        {
            if (clip.last <= frame) isRewinding = true;
            else if (frame == 0 && isRewinding) isRewinding = false;

            if (isRewinding) frame--;
            else frame++;
        }
        ren.sprite = clip.sprites[frame];
    }


    public void Play(DataAnimationClip c)
    {
        clip = c;
    }
    public void Play(string key)
    {
        enabled = true;
        clip = list.Find(e => e.key == key);
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
