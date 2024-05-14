using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    Transform tr;
    public Transform target;
    void Awake()
    {
        tr = transform;
    }

    void Update()
    {
        if (target)
            tr.position = target.position;
    }

    public void Init(Transform t)
    {
        target = t;
    }
}
