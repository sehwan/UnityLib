using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SlideInAndOut : MonoBehaviour
{
    public Vector2 startPosition = new(10, 0);
    public Vector2 endPosition = new(-10, 0);
    public float delay = 0;
    public float inTime = 0.5f;
    public float waitTime = 2f;
    public float outTime = 0.5f;
    public Ease inEase = Ease.OutExpo;
    public Ease outEase = Ease.OutExpo;

    Vector2 originPosition;

    void Awake()
    {
        originPosition = transform.localPosition;
    }
    void OnEnable()
    {
        transform.localPosition = startPosition;
        StartCoroutine(Co_Slide());
    }
    IEnumerator Co_Slide()
    {
        yield return new WaitForSeconds(delay);
        transform.DOLocalMove(originPosition, inTime).SetEase(inEase);
        yield return new WaitForSeconds(inTime + waitTime);
        transform.DOLocalMove(endPosition, outTime).SetEase(outEase);
        yield return new WaitForSeconds(outTime);
    }
}
