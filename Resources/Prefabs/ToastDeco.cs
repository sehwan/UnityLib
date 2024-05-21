using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToastDeco : MonoBehaviour
{
    public Text txt;


    public static void Show(string message, float duration = 1.5f)
    {
        var PATH = "Prefabs/ToastDeco";
        var _ = ((GameObject)Instantiate(Resources.Load(PATH))).GetComponent<ToastDeco>();

        _.txt.text = message;
        _.transform.DOScaleY(0, 0.3f).SetDelay(duration).OnComplete(() => Destroy(_.gameObject));
    }
}
