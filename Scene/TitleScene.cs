using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class TitleScene : MonoBehaviour
{
    public static TitleScene i;
    public Canvas canvas;
    // public Image img_title;
    public Text txt_version;
    public Text txt_message;
    public GameObject go_loading;
    public Button btn_start;
    public Text txt_session;
    public Text txt_record;

    bool isReady;

    public static TitleScene Instantiate() =>
         ((GameObject)Instantiate(Resources.Load("Prefabs/Title"))).GetComponent<TitleScene>();
    void Awake()
    {
        i = this;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = UM.i.cam;
        canvas.planeDistance = 0;
    }
    public void Start()
    {
        // BGM.i.clips_battle.Sample().PlayBGM();
        // BGM.i.clips_battle.PlayBGM();
        gameObject.SetActive(true);
        txt_version.text = $"v{Application.version}";

        btn_start.SetActive(false);
        go_loading.SetActive(false);
        txt_message.text = "Loading...";
        // RefreshInfo();

        GM.i.beforeInit += () =>
        {
            btn_start.SetActive(true);
            // EventSystem.current.firstSelectedGameObject = btn_start.gameObject;
            // EventSystem.current.SetSelectedGameObject(btn_start.gameObject);
            go_loading.SetActive(false);
            txt_message.text = "";
            isReady = true;
        };
        // img_title.material.SetFloat("_DissolveAmount", 1);
        // img_title.material.DOFloat(0, "_DissolveAmount", 2.9f);
        // GM.i.beforeInit += () =>
        // {
        // };

        // Prolog
        // var str = txt_prolog.text;
        // txt_prolog.text = null;
        // txt_prolog.DOText(str, str.Length * 0.1f).SetEase(Ease.Linear).SetDelay(1f);

        // Rankers
        // var rankers = PlayerPrefs.GetString("rankers").ToObject<RankData[]>();
        // rankItems = tr_rankers.GetComponentsInChildren<RankItem>();
        // for (int i = 0; i < rankItems.Length; i++)
        // {
        //     var e = rankItems[i];
        //     if (rankers == null || i >= rankers.Length)
        //     {
        //         e.gameObject.SetActive(false);
        //         continue;
        //     }
        //     e.Init(rankers[i], " " + "Stage".L());
        //     e.gameObject.SetActive(true);
        // }
    }
    void RefreshInfo()
    {
    }
    public void ResetData()
    {
        MessageBox.Show("TitleReset".L(), null, null, null, () =>
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        });
    }

    void Update()
    {
        // txt_message.text = FirebaseMng.inst.notice;
        if (isReady && Input.GetKeyDown(KeyCode.Space)) Btn_Start();
    }

    public void Btn_Start()
    {
        Destroy(gameObject);
        // Session.i.NewSession();
    }
    public void Btn_Quit()
    {
        Application.Quit();
    }

    public void Btn_Settings()
    {
        SettingsPanel.Show();
    }

    public int countWake;
    public void WakeReporter()
    {
        countWake++;
        if (countWake <= 15) return;
        // GameObject.Find("Reporter").GetComponent<Reporter>().enabled = true;
    }
}
