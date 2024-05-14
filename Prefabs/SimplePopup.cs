using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimplePopup : PrefabSignleton<SimplePopup>
{
    public static SimplePopup i
    {
        get
        {
            prefabPath = "Prefabs/SimplePopup";
            return instance;
        }
    }
    public GameObject go;
    public RectTransform frame;
    public Text title;
    public Text txt;
    public Image img;
    public Vector2? pos;

    public void Hide()
    {
        go.SetActive(false);
    }

    public void Show(string title, Sprite spr, string desc, float remain = 0, Vector2? pos = null)
    {
        i.CancelInvoke();
        if (title.IsNullOrEmpty() && spr == null && desc.IsNullOrEmpty()) return;

        go.SetActive(true);
        frame.localPosition = new Vector2(5000, 5000);

        this.title.SetActive(title.IsFilled());
        this.img.SetActive(spr != null);
        this.txt.SetActive(desc.IsFilled());
        if (title.IsFilled()) this.title.text = title;
        if (spr != null) this.img.sprite = spr;
        if (desc.IsFilled()) this.txt.text = desc.Trim();

        this.pos = pos;
        if (remain != 0) this.InvokeEx(Hide, remain);
        StartCoroutine(Co_Refresh());
    }
    IEnumerator Co_Refresh()
    {
        yield return null;
        if (pos.HasValue)
        {
            frame.localPosition = pos.Value;
            yield break;
        }
        Vector2 res = Def.RESOULUTION;
        Vector2 size = frame.GetComponent<RectTransform>().sizeDelta;
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
