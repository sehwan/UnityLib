using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlickerUI : MonoBehaviour
{
    public float PERIOD = 0.6f;
    public float MAX = 1;
    public float MIN = 0.4f;
    public float DELAY = 0;
    MaskableGraphic img;


    void Awake()
    {
        img = GetComponent<MaskableGraphic>();
    }


    void OnEnable()
    {
        StartCoroutine(Co_Flickering());
    }

    IEnumerator Co_Flickering()
    {
        yield return new WaitForSecondsRealtime(DELAY);
        WaitForSecondsRealtime w = new(PERIOD);
        while (true)
        {
            img.CrossFadeAlpha(MAX, PERIOD, true);
            yield return w;
            img.CrossFadeAlpha(MIN, PERIOD, true);
            yield return w;
        }
    }

    public IEnumerator Text_Co_Flickering()
    {
        img = GetComponent<MaskableGraphic>();
        float passed = 0;
        float w = 0;
        while (passed < 8)
        {
            w = 0;
            while ((w += Time.deltaTime) < PERIOD)
            {
                float lerp = Mathf.Lerp(MIN, MAX, w / PERIOD);
                Debug.Log($"<color=cyan>{w}/{lerp}</color>");
                img.SetAlpha(lerp);
                yield return null;
            }
            w = 0;
            while ((w += Time.deltaTime) < PERIOD)
            {
                float lerp = Mathf.Lerp(MAX, MIN, w / PERIOD);
                Debug.Log($"<color=cyan>{w}/{lerp}</color>");
                img.SetAlpha(lerp);
                yield return null;
            }
            passed += PERIOD * 2;
        }
    }

    public void StartFlickering()
    {
        StopAllCoroutines();
        StartCoroutine(Co_Flickering());
    }
    public void StopFlickering()
    {
        StopAllCoroutines();
    }
}
