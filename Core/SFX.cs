using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;

public class SFX : MonoBehaviour
{
    public static SFX i;
    static SerializedDictionary<string, AudioClip> dic = new();
    AudioSource[] audios;
    int length;
    int iter;

    const string dirPath = "SFX";
    [SerializeField] List<string> failedNames = new();


    [ContextMenu("Make Audios")]
    void MakeAudios()
    {
        for (int i = 0; i < 5; i++)
            gameObject.AddComponent<AudioSource>();
    }

    void Awake()
    {
        i = this;
        var dir = Resources.LoadAll<AudioClip>(dirPath);
        foreach (var file in dir)
            dic.Add(file.name, file);

        audios = GetComponents<AudioSource>();
        length = audios.Length;

        if (Settings.MuteSFX) SetVolume(0);
        else SetVolume(1);
    }

#if UNITY_EDITOR
    void OnDestroy()
    {
        if (failedNames.Count == 0) return;
        var sb = new StringBuilder();
        failedNames.ForEach(e => sb.Append($"{e}, "));
        Debug.Log($"<color=red>No SFX Files: {sb}</color>");
    }
#endif

    GameObject selected;
    void Update()
    {
        // play click sound Only when it's a button
        if (Input.GetMouseButtonDown(0) == false) return;
        selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null) return;

        var btn = selected.GetComponent<Button>();
        if ((btn != null && btn.transition != Selectable.Transition.None) ||
            selected.GetComponent<Toggle>() != null) Play("beep");
    }


    public static void Mute(bool b)
    {
        if (i == null) return;
        i.audios.ForEach(e =>
        {
            e.enabled = !b;
        });
    }
    public static void SetVolume(float val)
    {
        Mute(val == 0);
        if (i == null) return;
        i.audios.ForEach(e => e.volume = val);
    }

    public static void Play(string name)
    {
        if (i == null) return;
        if (name.IsNullOrEmpty()) return;
        if (dic.TryGetValue(name, out var clip) == false)
        {
            if (i.failedNames.Contains(name) == false) i.failedNames.Add(name);
            return;
        }
        if (i.iter >= i.length) i.iter = 0;
        i.audios[i.iter++]?.PlayOneShot(clip);
    }
}
