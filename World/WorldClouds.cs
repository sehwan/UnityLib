using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WorldClouds : MonoBehaviour
{
    [Header("Settings")]
    public int W = 40, H = 70;
    public float SPD_MAX, SPD_MIN;
    public float SCALE_MAX, SCALE_MIN;

    [Header("Wind")]
    public float wind_spd;
    public Vector3 wind_dir;


    Sprite[] sprites;
    Transform[] trs;
    SpriteRenderer[] rens;
    Vector3[] vel;
    float wMax, yMax;
    int count;


    void Awake()
    {
        RandomWind();
        count = transform.childCount;
        sprites = Resources.LoadAll<Sprite>("BG/clouds");
        rens = new SpriteRenderer[count];
        trs = new Transform[count];
        vel = new Vector3[count];
        wMax = W - 0.1f;
        yMax = H - 0.1f;

        for (int i = 0; i < count; i++)
        {
            trs[i] = transform.GetChild(i);
            trs[i].localPosition = new Vector2(RandomEx.R(W, -W), RandomEx.R(H, -H));
            rens[i] = trs[i].GetComponent<SpriteRenderer>();
            ResetCloud(i);
        }
    }


    void Update()
    {
        for (int i = 0; i < count; i++)
        {
            Transform e = trs[i];
            e.localPosition += Time.deltaTime * vel[i];

            //Out?
            if (Mathf.Abs(e.localPosition.x) > W || Mathf.Abs(e.localPosition.y) > H)
            {
                ResetCloud(i);
                var tr = trs[i];
                tr.localPosition = new Vector2(
                    Mathf.Clamp(-tr.localPosition.x, -wMax, wMax),
                    Mathf.Clamp(-tr.localPosition.y, -yMax, yMax));
            }
        }
    }

    void ResetCloud(int i)
    {
        vel[i] = wind_spd.RandomizeByPercents(50) * wind_dir;
        var x = RandomEx.R(SCALE_MAX, SCALE_MIN);
        var y = RandomEx.R(SCALE_MAX, SCALE_MIN) * 0.8f;
        if (y > x) y = x;
        trs[i].localScale = new Vector2(x, y);
        rens[i].sprite = sprites.Sample();
    }


    public void RandomWind()
    {
        wind_spd = RandomEx.R(SPD_MAX, SPD_MIN);
        wind_dir = Random.insideUnitCircle.normalized;
    }
}
