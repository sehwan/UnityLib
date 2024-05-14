using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveTimer : MonoBehaviour
{
    public float wait = 3.5f;


    WaitForSeconds w_wait;
    void Awake()
    {
        w_wait = new WaitForSeconds(wait);
    }
    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return w_wait;
        gameObject.SetActive(false);
    }
}
