using System;
using UnityEngine;
using UnityEngine.UI;

public class Common_LittleMenu : MonoBehaviour
{
    public RectTransform tr_grid;

    public void Hide()
    {
        Destroy(gameObject);
    }

    public static void Show(float width = 300, float height = 55, params (string, Action)[] menus)
    {
        var _ = Instantiate(Resources.Load<GameObject>("Prefabs/Common_LittleMenu"))
            .GetComponent<Common_LittleMenu>();

        // Positioning
        Vector2 resolution = new(Screen.width, Screen.height);
        Vector2 size_go = new(width, menus.Length * (height + 4));
        Vector2 pos_viewport = CameraWork.i.cam.ScreenToViewportPoint(Input.mousePosition);
        Vector2 pos_new = new(pos_viewport.x - 0.5f, pos_viewport.y - 0.5f);

        pos_new.x *= resolution.x;
        pos_new.y *= resolution.y;
        if (pos_new.x + size_go.x / 2 > resolution.x / 2)
            pos_new.x -= size_go.x / 2;
        else if (pos_new.x - size_go.x / 2 < 0)
            pos_new.x += size_go.x / 2;
        if (pos_new.y + size_go.y / 2 > resolution.y / 2)
            pos_new.y -= size_go.y / 2;
        else if (pos_new.y - size_go.y / 2 < 0)
            pos_new.y += size_go.y / 2;
        _.tr_grid.localPosition = pos_new;
        _.tr_grid.sizeDelta = size_go;

        var pf = _.tr_grid.GetChild(0).gameObject;
        for (int i = 0; i < menus.Length; i++)
        {
            var btn = i != 0 ? Instantiate(pf) : pf;
            btn.transform.SetParent(_.tr_grid);
            btn.transform.LocalScale(1);
            btn.transform.Find("Text").GetComponent<Text>().text = menus[i].Item1;
            btn.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                _.Hide();
                menus[i].Item2();
            });
        }
    }
}
