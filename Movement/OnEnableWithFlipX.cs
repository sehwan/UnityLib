using UnityEngine;
using DG.Tweening;

public class OnEnableWithFlipX : MonoBehaviour
{
    public float duration = 0.2f;

    void OnEnable()
    {
        transform.LocalScaleX(0);
        transform.DOScaleX(1,duration);
    }
}
