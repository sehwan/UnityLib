using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMSample : MonoBehaviour {
    public Transform target;
    public float speed;
    public float force;
    public float time;

    Vector3 vecForce;

	// Use this for initialization
	void Start () {
        vecForce = transform.up * force;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        dir += vecForce;
        dir.Normalize();

        vecForce *= 0.9f;

        transform.Translate(dir * speed * Time.deltaTime, Space.World);
        transform.up = dir;
	}
}
