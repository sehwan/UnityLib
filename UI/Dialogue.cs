using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


[System.Serializable]
public class MetaDialog
{
    public string key;
    public string bg;
    public string left;
    public string mid;
    public string right;
    public string teller;
    public string dialog;

    public MetaDialog(string b, string l, string m, string r, string t, string d)
    {
        bg = b;
        left = l;
        mid = m;
        right = r;
        teller = t;
        dialog = d;
    }
}



public class Dialogue : MonoSingleton<Dialogue>
{
    public GameObject go;
    public Image img_bg;
    public Image img_left;
    public Image img_mid;
    public Image img_right;
    public Text txt_name;
    public Text txt_dialog;
    public Button btn_next;
    // public _2dxFX_Blur blur;


    public override void Init()
    {
        base.Init();
        go = Instantiate(Resources.Load("Prefabs/dialog") as GameObject);
        go.transform.SetParent(UM.i.transform, false);
        go.SetActive(false);
        // References
        img_bg = go.Finds("bg").GetComponent<Image>();
        // blur = img_bg.GetComponent<_2dxFX_Blur>();
        img_left = go.Finds("left").GetComponent<Image>();
        img_mid = go.Finds("mid").GetComponent<Image>();
        img_right = go.Finds("right").GetComponent<Image>();
        txt_name = go.Finds("name/text").GetComponent<Text>();
        txt_dialog = go.Finds("dialog/text").GetComponent<Text>();
        // btn_next = go.Finds("dialog").GetComponent<Button>();
        btn_next = go.GetComponent<Button>();
    }


    public void SetBlur(bool b)
    {
        // blur.enabled = b;
    }
    public void Show(string b, string l, string m, string r, string t, string d, Action cb = null)
    {
        MetaDialog meta = new(b, l, m, r, t, d);
        Show(meta, cb);
    }
    public void Show(MetaDialog d, System.Action cb = null)
    {
        go.SetActive(true);

        //bg
        img_bg.SetActive(!d.bg.IsNullOrEmpty());
        if (!d.bg.IsNullOrEmpty()) img_bg.sprite = d.bg.LoadSprite();

        //Teller
        txt_name.transform.parent.SetActive(!d.teller.IsNullOrEmpty());
        txt_name.text = d.teller;
        if (d.teller.IsFilled())
        {
            img_left.color = Color.white;
            img_mid.color = Color.white;
            img_right.color = Color.white;
        }
        else
        {
            img_left.color = Color.gray;
            img_mid.color = Color.gray;
            img_right.color = Color.gray;
        }

        //dialog
        txt_dialog.DOText("", 0);
        txt_dialog.DOText(d.dialog, d.dialog.Length * 0.01f);
        // txt_dialog.text = d.dialog;

        //character
        //left
        img_left.SetActive(!d.left.IsNullOrEmpty());
        if (!d.left.IsNullOrEmpty()) img_left.sprite = d.left.LoadSprite();

        //mid
        img_mid.SetActive(!d.mid.IsNullOrEmpty());
        if (!d.mid.IsNullOrEmpty()) img_mid.sprite = d.mid.LoadSprite();

        //right
        img_right.SetActive(!d.right.IsNullOrEmpty());
        if (!d.right.IsNullOrEmpty()) img_right.sprite = d.right.LoadSprite();

        //button
        btn_next.onClick.RemoveAllListeners();
        btn_next.onClick.AddListener(() =>
        {
            if (txt_dialog.text != (d.dialog))
            {
                txt_dialog.DOKill();
                txt_dialog.DOText(d.dialog, 0);
                return;
            }
            callback = cb;
            // this.InvokeEx(Hide, f);
            Hide();
            btn_next.onClick.RemoveAllListeners();
        });
    }

    Action callback;
    public void Hide()
    {
        go.SetActive(false);
        callback?.Invoke();
    }
}
