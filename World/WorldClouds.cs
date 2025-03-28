using UnityEngine;

public class WorldClouds : MonoBehaviour
{
    [Header("Settings")]
    public float W = 40;
    public float H = 70;
    public float SPD_MAX = 3, SPD_MIN = 0.1f;
    public float SCALE_MAX = 10, SCALE_MIN = 1;
    public float yScaler = 0.7f;
    public Sprite[] sprites;

    [Header("Wind")]
    public float wind_spd;
    public Vector2 wind_dir;

    // Calculaated
    Transform[] trs;
    SpriteRenderer[] rens;
    Vector2[] vel;
    float wMax, yMax;
    int count;


    void Awake()
    {
        RandomWind();
        count = transform.childCount;
        // sprites = Resources.LoadAll<Sprite>("BG/clouds");
        rens = new SpriteRenderer[count];
        trs = new Transform[count];
        vel = new Vector2[count];
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
            e.localPosition += Time.deltaTime * vel[i].V3();

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
        var y = RandomEx.R(SCALE_MAX, SCALE_MIN);
        if (y > x) y = x;
        trs[i].localScale = new Vector2(x, y * yScaler);
        if (sprites.Length > 0) rens[i].sprite = sprites.Sample();
    }
    
    [ContextMenu("Test")]
    public void Test()
    {
        Awake();
    }

    public void RandomWind()
    {
        wind_spd = RandomEx.R(SPD_MAX, SPD_MIN);
        wind_dir = Random.insideUnitCircle.normalized;
    }
}
