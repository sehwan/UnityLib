using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollower : MonoBehaviour
{
    public Transform target;
    public float maxSpeed;
    public float smoothTime;
    public Vector3 offset;
    Vector3 vector;



    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            target.TransformPoint(offset),
            ref vector,
            smoothTime,
            maxSpeed,
            Time.deltaTime);
    }
}
