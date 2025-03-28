using UnityEngine;

public class IdleScalerY : MonoBehaviour
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
        float scaleFactor = 1 + Mathf.Sin((Time.time + random) / period) * range;
        tr.localScale = new Vector2(originScale.x, originScale.y * scaleFactor);
    }
}
