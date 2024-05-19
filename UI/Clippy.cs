using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Clippy : PrefabSignleton<Clippy>
{
    public static Clippy Inst()
    {
        prefabPath = "Prefabs/Clippy";
        return instance;
    }

    public GraphicRaycaster raycaster;
    public RectTransform clippy;
    public Image image;
    public Text txt;
    public Button btn;

    Vector2 pos_origin;
    Action callback;



    public override void Init()
    {
        base.Init();
        pos_origin = clippy.anchoredPosition;
    }

    public void Show(Sprite sprite, string str, Vector2? pos = null, Action cb = null)
    {
        gameObject.SetActive(true);
        image.sprite = sprite;
        txt.text = null;
        txt.DOText(str, 0.1f, true);

        if (pos != null) clippy.anchoredPosition = pos.Value;
        else clippy.anchoredPosition = pos_origin;

        raycaster.enabled = cb != null;
        callback = cb;
    }
    public void Show(string spritePath, string str, Vector2? pos = null, Action cb = null)
    {
        Show(spritePath.LoadSprite(), str, pos, cb);
    }

    // to Button
    public void Next()
    {
        callback?.Invoke();
    }

    // Manual
    public void Hide()
    {
        gameObject.SetActive(false);
        callback?.Invoke();
    }
}
