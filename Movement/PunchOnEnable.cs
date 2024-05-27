using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
public class PunchOnEnable : MonoBehaviour
{
    public float duration = 0.1f;
    public float strength = -0.2f;
    public int vibrato = 6;
    [Range(0, 1)] public float elasticity = 1;


    public void OnEnable()
    {
        var tr = transform;
        tr.DOPunchScale(new Vector3(strength, strength, 0), duration, vibrato, elasticity).SetUpdate(true)
            .OnComplete(() => tr.localScale = Vector2.one);
    }
    void OnDisable()
    {
        transform.DOKill();
    }
}