using UnityEngine;
using UnityEngine.UI;
using System;

public class PopInput : MonoSingleton<PopInput>
{
    //내부.
    public GameObject box;
    Text txt_title;
    InputField input;


    //데이터.
    Action<MessageBoxResult, string> cb;



    new public void Init()
    {
        DontDestroyOnLoad(gameObject);

        if (box == null)
        {
            box = (GameObject)Instantiate(Resources.Load("Prefabs/PopInput"));
            box.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
        box.SetActive(false);

        Transform tr = box.transform;
        tr.Find("dim").GetComponent<Button>().onClick.AddListener(ESC);
        tr.Find("frame/btn_ok").GetComponent<Button>().onClick.AddListener(Btn_Ok);
        txt_title = tr.Find("frame/title/txt_message").GetComponent<Text>();
        Transform tr_contents = tr.Find("frame/Contents");
        input = tr_contents.Find("InputField").GetComponent<InputField>();
    }



    void Btn_Ok()
    {
        NotifyResult(MessageBoxResult.OK);
    }
    public void ESC()
    {
        NotifyResult(MessageBoxResult.NO);
    }
    public void NotifyResult(MessageBoxResult result)
    {
        box.SetActive(false);
        // Um.inst._showingWindows.Remove(box);
        cb?.Invoke(result, input.text);
    }



    public void Show(string title, string initial, Action<MessageBoxResult, string> cb)
    {
        if (box == null) Init();

        // Um.inst.ShowWindow(box);
        box.SetActive(true);
        box.transform.SetAsLastSibling();

        txt_title.text = title;
        input.text = initial;
        this.cb = cb;
    }
}
