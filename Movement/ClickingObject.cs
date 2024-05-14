using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickingObject : MonoBehaviour
{
    public float _delay = 5f;
    public float _duration = 0.6f;
    public float _power = 1.5f;


    void OnEnable()
    {
        StartCoroutine(Co_Clicking());
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }


    IEnumerator Co_Clicking()
    {
        yield return new WaitForSeconds(Random.Range(0, _delay));
        WaitForSeconds w = new(_delay);
        while (true)
        {
            transform.DOPunchRotation(Vector3.forward * _power, _duration);
            yield return w;
        }
    }
}
