using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCurved : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float force;

    Vector3 vecForce;
	public float time;



    void Start()
    {
        vecForce = transform.up * force;
    }


	//목표 벡터 + 간섭 벡터(sin, cos, log) 등 추가.
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir += vecForce;
        dir.Normalize();
		vecForce -= vecForce * Time.deltaTime * time;


        transform.Translate(dir * speed * Time.deltaTime, Space.World);
        transform.up = dir;
    }
}
