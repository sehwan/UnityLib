using System.Collections;
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
    public Text title;
    public Text txt;
    public Image img;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(string title, Sprite spr, string desc)
    {
        i.CancelInvoke();
        if (title.IsNullOrEmpty() && spr == null && desc.IsNullOrEmpty()) return;

        gameObject.SetActive(true);
        frame.localPosition = new Vector2(5000, 5000);

        this.title.SetActive(title.IsFilled());
        this.img.SetActive(spr != null);
        this.txt.SetActive(desc.IsFilled());
        if (title.IsFilled()) this.title.text = title;
        if (spr != null) this.img.sprite = spr;
        if (desc.IsFilled()) this.txt.text = desc.Trim();

        StartCoroutine(Co_Refresh());
    }
    IEnumerator Co_Refresh()
    {
        yield return null;
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
