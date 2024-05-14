
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    static public Loading inst;
    public GameObject go;
    public Text txt;
    public GameObject circle;


    void Awake()
    {
        inst = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Show(string text = "")
    {
        go.SetActive(true);
        if (text.IsFilled()) txt.text = text;
        else txt.text = Def.RandomTip();
        this.InvokeEx(ShowDetail, 1f);
    }
    void ShowDetail()
    {
        txt.SetActive(true);
        circle.SetActive(true);
    }

    public void Hide()
    {
        go.SetActive(false);
        txt.SetActive(false);
        circle.SetActive(false);
    }

    void RefreshText()
    {
        txt.text = Def.RandomTip();
    }

    public int countWake;
    public void WakeReporter()
    {
        countWake++;
        if (countWake <= 20) return;
        // GameObject.Find("Reporter").GetComponent<Reporter>().enabled = true;
        // FirebaseMng.Log("wake_reporter", "count", countWake);
    }
}
