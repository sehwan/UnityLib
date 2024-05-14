using UnityEngine;
using DG.Tweening;

public class ShrinkOnEnable : MonoBehaviour
{
    public float duration = 0.3f;
    public float startingScale = 8;


    void OnEnable()
    {
        Transform tr = transform;
        tr.LocalScale(startingScale);
        tr.DOScale(Vector2.one, duration).SetEase(Ease.OutElastic);
    }
}
