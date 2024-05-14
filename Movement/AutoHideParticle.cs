using UnityEngine;
using System.Collections;

public class AutoHideParticle : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Hide", GetComponent<ParticleSystem>().main.duration);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}