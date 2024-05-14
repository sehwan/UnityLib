using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageTweenTMP : MonoBehaviour
{
    public TextMeshPro txt;
    public float MaxScale = 2.5f;
    public float Duration = 0.5f;
    public float Speed = 0.2f;

    WaitForSeconds w_wait;
    Transform tr;

    void Awake()
    {
        tr = transform;
        w_wait = CoroutineEx.GetWait(Duration / 2);
    }
    void OnEnable()
    {
        StartCoroutine(Co_Tween());
    }
    IEnumerator Co_Tween()
    {
        gameObject.SetActive(true);
        txt.CrossFadeAlpha(1, 0, true);
        tr.LocalScale(MaxScale);

        // Tween
        tr.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutBack);
        yield return w_wait;

        // Fade Out
        txt.CrossFadeAlpha(0, Duration / 3, true);
        yield return w_wait;
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        tr.DOKill();
    }

    void Update()
    {
        tr.position += Speed * Time.deltaTime * Vector3.up;
    }
}
