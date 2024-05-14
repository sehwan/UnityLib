using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationOnEnable2D : MonoBehaviour
{
    void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
}
