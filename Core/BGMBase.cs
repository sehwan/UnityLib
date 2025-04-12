using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMBase : MonoBehaviour
{
    public static BGMBase i;
    AudioSource _audio;
    public AudioClip[] clips_lobby;
    public AudioClip[] clips_stage;


    protected virtual void Awake()
    {
        i = this;
        _audio = GetComponent<AudioSource>();
        _audio.loop = true;
        if (Settings.MuteBGM) SetVolume(0);
        else SetVolume(Settings.VolumeBGM);
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
        if (co_shuffle != null) StopCoroutine(co_shuffle);
        if (_audio.clip == clip) return;
        _audio.clip = clip;
        _audio.Play();
    }

    public void Play(AudioClip[] list)
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
        yield return new WaitForSecondsRealtime(length);
        Play(list);
    }
}