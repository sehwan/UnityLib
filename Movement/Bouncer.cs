using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bouncer : MonoBehaviour
{
    public float power = 7f;
    public float duration = 0.3f;
    public float delay = 1.5f;
    public float startDelay = 1;
    [Range(0, 1)] public float randomness = 0f;


    void OnEnable()
    {
        StartCoroutine(Bounce());
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator Bounce()
    {
        yield return new WaitForSeconds((Random.Range(0, startDelay)));
        WaitForSeconds time = new(delay * Random.Range(0, randomness) + delay + duration);
        while (true)
        {
            transform.DOLocalJump(transform.localPosition, power, 1, duration);
            yield return time;
        }
    }
}
