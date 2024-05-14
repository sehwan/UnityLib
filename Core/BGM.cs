using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGM : MonoSingletonDontDestroyed<BGM>
{
    AudioSource _audio;
    public AudioLowPassFilter lowPassFilter;
    public AudioClip[] clips_battle;


    protected override void Awake()
    {
        base.Awake();
        _audio = GetComponent<AudioSource>();
        _audio.loop = true;
        if (Settings.MuteBGM) SetVolume(0);
        else //SetVolume(1);
            SetVolume(Settings.VolumeBGM);
    }

    public static bool IsMute()
    {
        return i._audio.enabled;
    }
    public static void Mute(bool b)
    {
        if (i == null) return;
        i._audio.enabled = !b;
    }
    public static void SetVolume(float val)
    {
        if (i == null) return;
        Mute(val == 0);
        i._audio.volume = val;
    }
    public static void Stop()
    {
        i._audio.Stop();
    }

    public void Play(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }

    public void PlayListShuffle(AudioClip[] list)
    {
        SetVolume(Settings.VolumeBGM);
        var sample = list.Sample();
        while (sample == _audio.clip) sample = list.Sample();
        _audio.clip = sample;
        _audio.Play();
        if (co_shuffle != null) StopCoroutine(co_shuffle);
        co_shuffle = StartCoroutine(Co_Shuffle(list, sample.length));
    }
    Coroutine co_shuffle;
    IEnumerator Co_Shuffle(AudioClip[] list, float length)
    {
        yield return new WaitForSeconds(length);
        PlayListShuffle(list);
    }
}


public static class AudioClipEx
{
    public static void PlayBGM(this AudioClip me)
    {
        BGM.i.Play(me);
    }
    public static void PlayBGM(this AudioClip[] list)
    {
        BGM.i.PlayListShuffle(list);
    }
}