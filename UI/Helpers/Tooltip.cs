using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : PrefabSignleton<Tooltip>
{
    public static Tooltip i
    {
        get
        {
            prefabPath = "Prefabs/Tooltip";
            return instance;
        }
    }
    public RectTransform frame;
    public TextMeshProUGUI title;
    public TextMeshProUGUI txt;
    public Image img;
    public Action onHide;


    protected override void Awake()
    {
        base.Awake();
        GetComponent<GraphicRaycaster>().enabled = Application.isMobilePlatform;
    }

    public void OnDisable()
    {
        onHide?.Invoke();
        onHide = null;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    async public Awaitable Show(string titleString, Sprite spr, string desc)
    {
        i.CancelInvoke();
        if (titleString.IsNullOrEmpty() && spr == null && desc.IsNullOrEmpty()) return;

        gameObject.SetActive(true);
        frame.localPosition = new Vector2(5000, 5000);

        title.SetActive(titleString.IsFilled());
        img.SetActive(spr != null);
        txt.SetActive(desc.IsFilled());
        if (titleString.IsFilled()) title.text = titleString;
        if (spr != null) img.sprite = spr;
        if (desc.IsFilled()) txt.text = desc.Trim();

        await Awaitable.NextFrameAsync();
        Vector2 size = frame.sizeDelta;
        Vector2 res = GetComponent<CanvasScaler>().referenceResolution;
        Vector2 pos_viewport = UM.i.cam.ScreenToViewportPoint(Input.mousePosition);
        Vector2 newPos = new(pos_viewport.x - 0.5f, pos_viewport.y - 0.5f);
        newPos.x *= res.x;
        newPos.y *= res.y;
        var DIV = 2.0f;
        newPos.x = Mathf.Clamp(newPos.x, -res.x / DIV + size.x / DIV, res.x / DIV - size.x / DIV);
        newPos.y = Mathf.Clamp(newPos.y, -res.y / DIV + size.y / DIV, res.y / DIV - size.y / DIV);
        frame.localPosition = newPos;
    }
}