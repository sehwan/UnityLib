using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RestartParticleOnEnable : MonoBehaviour
{
    ParticleSystem particle;
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        particle.time = 0;
        particle.Emit(1);
        particle.Play();
    }
}
