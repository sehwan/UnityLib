using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroy : MonoBehaviour
{
    public float wait = 1;

    void OnEnable()
    {
        Destroy(gameObject);
    }
    
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
