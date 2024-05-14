using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}