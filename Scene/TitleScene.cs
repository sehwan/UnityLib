using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public Canvas canvas;
    public Text txt_version;
    public Text txt_message;
    public GameObject go_loading;
    public Button btn_start;

    bool isReady;

    public static TitleScene Instantiate() =>
         ((GameObject)Instantiate(Resources.Load("Prefabs/Title"))).GetComponent<TitleScene>();


    public virtual void Start()
    {
        gameObject.SetActive(true);
        txt_version.text = $"v{Application.version}";

        btn_start.SetActive(false);
        go_loading.SetActive(false);
        txt_message.text = "Loading...";

        GM.i.beforeInit += () =>
        {
            btn_start.SetActive(true);
            go_loading.SetActive(false);
            txt_message.text = "";
            isReady = true;
        };
    }


    public virtual void _ResetData()
    {
        MessageBox.Show("TitleReset".L(), null, null, null, () =>
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        });
    }

    public virtual void Update()
    {
        if (isReady && Input.GetKeyDown(KeyCode.Space)) _Start();
    }

    public virtual void _Start()
    {
        gameObject.SetActive(false);
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
