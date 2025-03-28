using UnityEngine;

public class IdleScaler : MonoBehaviour
{
    public float range = 0.03f;
    public float period = 0.3f;
    public bool isRandomize = true;
    float random = 0;
    Vector2 originScale;
    Transform tr;

    void Awake()
    {
        tr = transform;
        originScale = transform.localScale;
        if (isRandomize) random = Random.Range(0, period);
    }

    void Update()
    {
        tr.localScale = originScale +
            Mathf.Sin((Time.time + random) / period) * range * Vector2.one;
    }
}
