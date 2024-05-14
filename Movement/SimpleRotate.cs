using System.Collections;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public float interval = 0.05f;
    public float _rotate = -3;


    void OnEnable()
    {
        StartCoroutine(Rotate());
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator Rotate()
    {
        WaitForSeconds w = new(interval);
        while (true)
        {
            transform.Rotate(0, 0, -_rotate);
            yield return w;
        }
    }
}
