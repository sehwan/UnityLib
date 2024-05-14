using UnityEngine;

public class FlickerSprite : MonoBehaviour
{
    public float PERIOD = 1;
    public float MAX = 1;
    public float MIN = 0.5f;
    float random;
    SpriteRenderer ren;


    void Awake()
    {
        ren = GetComponent<SpriteRenderer>();
        random = Random.Range(0, PERIOD);
    }


    void Update()
    {
        var wave = NumberEx.TriangularWave(random + Time.time / PERIOD);
        var alpha = Mathf.Lerp(MIN, MAX, wave);
        ren.SetAlpha(alpha);
    }
}
