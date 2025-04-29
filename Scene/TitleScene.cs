using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public Action onStart;
    bool isReady;

    public static TitleScene i;
    public Canvas canvas;
    public Text txt_version;
    public Text txt_message;
    public GameObject go_loading;
    public Button btn_start;
    


    public static TitleScene Instantiate() =>
         ((GameObject)Instantiate(Resources.Load("Prefabs/Title"))).GetComponent<TitleScene>();

    void Awake()
    {
        i = this;
        canvas.worldCamera = Camera.main;
    }

    public virtual void Start()
    {
        if (isReady) return;

        txt_version.text = $"v{Application.version}";
        txt_message.text = "Loading...";
        btn_start.SetActive(false);
        go_loading.SetActive(true);

        GM.i.beforeInit += OnInit;
    }
    public void OnInit()
    {
        isReady = true;
        txt_message.text = "";
        btn_start.SetActive(true);
        go_loading.SetActive(false);
    }


    public virtual void _ResetData()
    {
        MessageBox.Show("TitleReset".L(), null, null, null, () =>
        {
            PlayerPrefs.DeleteAll();
            // SceneManager.LoadScene(0);
        });
    }

    public virtual void Update()
    {
        if (isReady && Input.GetKeyDown(KeyCode.Space)) SendMessage("_Start");
    }

    public virtual void _Start()
    {
        gameObject.SetActive(false);
        onStart?.Invoke();
    }
    public virtual void _Quit()
    {
        Application.Quit();
    }

    public virtual void _Settings()
    {
        SettingsPanel.Show();
    }

    public int countWake;
    public virtual void _WakeReporter()
    {
        countWake++;
        if (countWake <= 15) return;
#if REPORTER
        GameObject.Find("Reporter").GetComponent<Reporter>().enabled = true;
#endif
    }
}
