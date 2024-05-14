using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
public class PopOnEnable : MonoBehaviour
{
    public float delay = 0;
    public float duration = 0.07f;
    public float strength = 0.07f;
    public bool zeroStart;

    public void OnEnable()
    {
        var tr = transform;
        if (zeroStart) tr.localScale = Vector2.zero;
        else tr.localScale = Vector2.one * 3;
        tr.DOScale(Vector2.one, 0.1f).SetDelay(delay).SetUpdate(true)
            .OnComplete(() => tr.DOShakeScale(duration, strength, 1)
                .OnComplete(() => tr.localScale = Vector2.one));
    }
    void OnDisable()
    {
        transform.DOKill();
    }
}
