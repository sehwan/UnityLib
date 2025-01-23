using System.Collections;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float RANGE = 0.1f;
    public float PERIOD = 0.3f;
    float random = 0;
    float time = 0;
    Transform tr;


    void Start()
    {
        tr = transform;
        // to avoid same movement of all the objects with this script 
        time += Random.Range(0, PERIOD);
    }

    public void Update()
    {
        // time += Time.deltaTime;
        tr.localPosition = new Vector2(
            0,
            Mathf.Sin(Time.time / PERIOD) * RANGE);
    }

    // public void UnRandomize()
    // {
    //     random = 0;
    // }


    public IEnumerator Co_TestPlay()
    {
        tr = transform;
        var passed = 0f;
        var duration = Mathf.Max(3f, PERIOD * 10);
        while (passed < duration)
        {
            passed += Time.deltaTime;
            Update();
            yield return null;
        }
    }
}