using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenScoring : MonoBehaviour
{
    Text _text;
    public int _cur;
    public int _end;
    public int MAX_STEP = 50;
    public float STEP_TIME = 0.05f;

    void OnDisable()
    {
        StopAllCoroutines();
    }


    void Awake()
    {
        _text = GetComponent<Text>();
    }


    public void Add_Tweening(int start, int end)
    {
        _cur = start;
        _end = end;
        StopAllCoroutines();
        if (gameObject.activeSelf) StartCoroutine(Co_Tween(start, end));
    }

    public int step;
    IEnumerator Co_Tween(int start, int end)
    {
        step = Mathf.Max((end - start) / MAX_STEP, 1);
        WaitForSeconds w = new(STEP_TIME);
        while (_cur < end - step)
        {
            _cur += step;
            _text.text = _cur.ToString();
            yield return w;
        }
        _cur = end;
        _text.text = _cur.ToString();
    }
}
