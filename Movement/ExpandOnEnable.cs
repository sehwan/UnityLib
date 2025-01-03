using UnityEngine;
using DG.Tweening;

public class ExpandOnEnable : MonoBehaviour
{
    public float duration = 0.15f;
    public bool x = true;
    public bool y = true;

    void OnEnable()
    {
        transform.localScale = new Vector2(x ? 0 : 1, y ? 0 : 1);
        transform.DOScale(Vector2.one, duration);
    }
}
