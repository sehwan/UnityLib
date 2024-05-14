using UnityEngine;

public class IdleScaler : MonoBehaviour
{
    public float range = 0.3f;
    public float period = 0.4f;
    float random = 0;
    Vector3 originScale;

    void Start()
    {
        originScale = transform.localScale;
        //to avoid same movement of all the objects with this script 
        random = Random.Range(0, 100);
    }

    void Update()
    {
        // transform.localScale = originScale +
        //     Vector3.one * Mathf.Sin((Time.time + random) / period) * range;
        transform.localScale = originScale +
            Mathf.Sin((Time.time + random) / period) * range * Vector3.one;
        // Mathf.PingPong(Time.time, 1)
        //핑퐁 사용하면 나으려나?
    }
}
