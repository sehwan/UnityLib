using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum MessageBoxResult { OK, NO, TIMEOUT, }

public class MessageBox : MonoBehaviour
{
    public RectTransform frame;
    public Text txt;
    public Image img;
    public Transform tr_contents;
    public GameObject go_require;
    public Image img_require;
    public Text txt_require;
    public Button btn_cancel;

    [Header("Data")]
    Action cb;
    Rsc rsc;
    GameObject displayingObj;


    // OK
    public void Btn_Ok()
    {
        if (rsc != null && rsc.n != 0 && rsc.isJustShowing == false)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                ToastGroup.Show("Need_Check_Internet".L());
                return;
            }
            if (rsc.n < 0 && rsc.IsPayable() == false)
            {
                ToastGroup.Show("NotEnoughResource".L());
                return;
            }
            // User.inst.AddResource(rsc);
        }
        NotifyResult(MessageBoxResult.OK);
    }
    // NO
    public void Btn_Cancel()
    {
        NotifyResult(MessageBoxResult.NO);
    }
    // End
    public void NotifyResult(MessageBoxResult result)
    {
        if (cb != null && result == MessageBoxResult.OK) cb();
        Destroy(gameObject);
    }

    // For Tutorial
    public void SetCloseButtonEnable(bool b)
    {
        btn_cancel.interactable = b;
    }
    void OnDestroy()
    {
        UM.i.others.Remove(gameObject);
    }


    public static void Show(
        string message,
        Rsc resourceAmount,
        Sprite image,
        GameObject go,
        Action callback)
    {

        var PATH = "Prefabs/MessageBox";
        var _ = Instantiate(Resources.Load<GameObject>(PATH)).GetComponent<MessageBox>();

        _.gameObject.SetActive(true);
        _.cb = callback;

        // Contents
        _.txt.text = message;
        _.img.SetActive(image != null);
        if (image != null) _.img.sprite = image;
        if (go != null)
        {
            _.displayingObj = go;
            go.transform.SetParent(_.tr_contents, false);
            go.transform.position = Vector2.zero;
            go.transform.SetAsFirstSibling();
        }

        // Resources
        _.rsc = resourceAmount;
        if (_.rsc != null && _.rsc.n != 0)
        {
            bool notPayable =
                _.rsc.isJustShowing == false &&
                _.rsc.n < 0 &&
                _.rsc.IsPayable() == false;

            _.go_require.gameObject.SetActive(true);
            // _.img_require.sprite = UIUtil.GetIcon(_.rsc.t);
            _.img_require.sprite = _.rsc.t.GetIcon();
            _.txt_require.text = $"{_.rsc.n:n0}";
            _.txt_require.color = notPayable ? Color.red : Color.white;
        }
        else _.go_require.gameObject.SetActive(false);

        // Refresh
        LayoutRebuilder.ForceRebuildLayoutImmediate(_.frame);


        UM.i.others.Add(_.gameObject);
    }
}
