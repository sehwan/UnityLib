using System.Collections;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float RANGE = 0.1f;
    public float PERIOD = 0.3f;
    float random = 0;
    float time = 0; // Don't use Time.time, it's not accurate. 
    Transform tr;


    void Start()
    {
        tr = transform;
        // to avoid same movement of all the objects with this script 
        random = Random.Range(0, PERIOD);
        // time += random;
    }

    public void Update()
    {
        tr.localPosition += new Vector3(
            0,
            Mathf.Sin((Time.time + random) / PERIOD)
            * RANGE);

        // time += Time.deltaTime;
        // tr.localPosition = new Vector2(
        //     tr.localPosition.x,
        //     Mathf.Sin(time / PERIOD)
        //     * RANGE);

        // it's affected by time scale. why? i don't know
        // tr.localPosition += Vector3.up
        //     * Mathf.Sin(time / PERIOD)
        //     * RANGE;
    }

    // Set start position to 0
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