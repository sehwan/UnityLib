using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FloatingTextTween : MonoBehaviour
{
    public Transform tr;
    public Text txt;
    public Image img;
    public float dur = 0.5f;
    public float durFade = 0.3f;
    // public float MaxScale = 2;


    void OnEnable()
    {
        StartCoroutine(Co_Tween());
    }

    IEnumerator Co_Tween()
    {
        // tr.LocalScale(MaxScale);
        // Tween
        // tr.DOScale(Vector2.one, 0.23f).SetEase(Ease.InOutBack);
        txt.CrossFadeAlpha(1, 0, true);
        img?.CrossFadeAlpha(1, 0, true);
        yield return CoroutineEx.GetWait(dur);

        // Fade
        txt.CrossFadeAlpha(0, durFade, true);
        img?.CrossFadeAlpha(0, durFade, true);
        yield return CoroutineEx.GetWait(durFade);
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        txt.DOKill();
        img?.DOKill();
    }
}
