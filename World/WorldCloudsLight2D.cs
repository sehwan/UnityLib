using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldCloudsLight2D : MonoBehaviour
{
    [Header("Settings")]
    public float W = 40;
    public float H = 70;
    public float SPD_MAX = 3, SPD_MIN = 0.1f;
    public float SCALE_MAX = 10, SCALE_MIN = 1;
    public float SCALE_DIFF = 2f;
    public Sprite[] sprites;

    [Header("Wind")]
    public float wind_spd;
    public Vector2 wind_dir;

    // Calculaated
    Transform[] trs;
    Light2D[] lights;
    Vector2[] vel;
    float wMax, yMax;
    int count;


    bool isInit;
    public void Init()
    {
        if (isInit) return;
        isInit = true;

        RandomWind();
        count = transform.childCount;
        sprites = Resources.LoadAll<Sprite>("BG/clouds");
        lights = new Light2D[count];
        trs = new Transform[count];
        vel = new Vector2[count];
        wMax = W - 0.1f;
        yMax = H - 0.1f;

        for (int i = 0; i < count; i++)
        {
            trs[i] = transform.GetChild(i);
            trs[i].localPosition = new Vector2(RandomEx.R(W, -W), RandomEx.R(H, -H));
            lights[i] = trs[i].GetComponent<Light2D>();
            ResetCloud(i);
        }
    }
    void Awake()
    {
        Init();
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
        var e = lights[i];
        e.pointLightOuterRadius = RandomEx.R(SCALE_MAX, SCALE_MIN);
        e.pointLightInnerRadius = e.pointLightOuterRadius - SCALE_DIFF;
        // if (sprites.Length > 0) lights[i].spri = sprites.Sample();
    }


    public void RandomWind()
    {
        wind_spd = RandomEx.R(SPD_MAX, SPD_MIN);
        wind_dir = Random.insideUnitCircle.normalized;
    }

    internal void SetColor(Color color)
    {
        foreach (var e in lights)
        {
            e.color = color;
        }
    }
}
