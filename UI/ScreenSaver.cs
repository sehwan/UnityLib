using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSaver : MonoBehaviour
{
    public static ScreenSaver inst;

#if UNITY_EDITOR
    public const int TIME_OUT = 1000;
#else
    public const int TIME_OUT = 300;
#endif


    public int afk;
    public int clicked;
    public bool isActive;

    public Image dim;
    public GameObject[] gos_click;

    // Default
    public Text txt_time;
    public Text txt_battery;

    [Header("For This Game only")]
    public Text txt_soul;
    public Text txt_gold;
    public Text txt_stage;


    WaitForSeconds w = new(1);
    IEnumerator Start()
    {
        inst = this;
        isActive = true;
        SetActive(false);
        while (true)
        {
            // Refresh
            if (isActive) RefreshHUDs();
            yield return w;
            afk++;
            // Make Active
            if (isActive == false)
            {
                if (afk >= TIME_OUT && Settings.NoScreenSaver == false)
                {
                    afk = 0; // For Reset clicked timing
                    SetActive(true);
                }
            }
            // Reset Click Counter Periodically
            else if (afk % 4 == 0)
            {
                clicked = 0;
                Redraw_ClickChecker();
            }
        }
    }

    void RefreshHUDs()
    {
        txt_battery.text = SystemInfo.batteryLevel.ToString("p0");
        txt_time.text = System.DateTime.Now.ToString("HH:mm:ss");
        // Specipic
        var userData = User.data;
        txt_soul.text = userData.soul.ToString();
        // txt_stage.text = UM.Scene<MainScenePanel>().txt_stage.text;
        // var uicamp = UM.Scene<UICamp>();
        // txt_standing.text = uicamp.txt_standing.text;
    }

    // Check Click
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == false) return;
        afk = 0;

        if (isActive == false) return;
        // Click
        clicked++;
        Redraw_ClickChecker();

        // Wake?
        if (clicked < 5) return;
        SetActive(false);
    }

    // Show & Hide
    public void SetActive(bool b)
    {
        if (isActive == b) return;
        if (b) RefreshHUDs();
        isActive = b;
        dim.SetActive(b);
        clicked = 0;
        Redraw_ClickChecker();

        // Optimization
        // UM.inst.GetComponent<Canvas>().enabled = !b;
        if (isActive) Application.targetFrameRate = 15;
        else Application.targetFrameRate = Settings.FrameRate;
    }

    void Redraw_ClickChecker()
    {
        for (int i = 0; i < 4; i++)
        {
            gos_click[i].SetActive(i < clicked);
        }
    }
}
