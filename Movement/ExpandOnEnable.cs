using UnityEngine;
using DG.Tweening;

public class ExpandOnEnable : MonoBehaviour
{
    public float duration = 0.15f;


    void OnEnable()
    {
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector2.one, duration);
    }
}
