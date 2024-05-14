using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleHoming : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float rotateSpeed = 200f;

    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 dir = ((Vector2)target.position - rb.position).normalized;
        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rb.angularVelocity = rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }
}
