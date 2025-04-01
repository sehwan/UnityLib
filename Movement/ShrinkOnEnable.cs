using UnityEngine;
using DG.Tweening;

public class ShrinkOnEnable : MonoBehaviour
{
    public float duration = 0.3f;
    public float startingScale = 8;
    public float delay = 0;


    void OnEnable()
    {
        Transform tr = transform;
        if (delay > 0)
        {
            transform.localScale = Vector2.zero;
            DOVirtual.DelayedCall(delay, () =>
            {
                Shrink();
            });
            return;
        }
        Shrink();
    }
    
    void Shrink()
    {
        Transform tr = transform;
        tr.localScale = Vector2.one * startingScale;
        tr.DOScale(Vector2.one, duration).SetEase(Ease.OutElastic);
    }
}
