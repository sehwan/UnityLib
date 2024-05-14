
//공용 인풋필드.
//Common_InputField.inst.Show("콜백함수", "메세지", "초기 텍스트");


using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Common_InputField : MonoSingleton<Common_InputField>
{
    public UIWindow w;
    public string _newValue;
    public Text txt_limit;

    Action<String> cb;



    override public void Init()
    {
        if (w == null)
        {
            var o = (GameObject)Instantiate(Resources.Load("Prefabs/Common_InputField"));
            w = o.gameObject.AddComponent<UIWindow>();
            w.transform.SetParent(UM.i.transform, false);
            w.transform.Find("btn_ok").GetComponent<Button>().onClick.AddListener(Btn_Ok);
            w.transform.Find("btn_cancel").GetComponent<Button>().onClick.AddListener(Btn_Cancel);
        }
    }


    public void Show(Action<string> callback, string message,
        string initial = "Enter text...", bool esc = true, int limit = 0)
    {
        UM.i.showings.Add(w);
        w.SetActive(true);
        cb = callback;
        w.transform.Find("txt_msg").GetComponent<Text>().text = message;
        w.transform.Find("InputField/Placeholder").GetComponent<Text>().text = initial;
        w.transform.Find("btn_cancel").gameObject.SetActive(esc);
        var txt_limit = w.transform.Find("txt_limit");
        if (limit == 0) txt_limit.SetActive(false);
        else txt_limit.GetComponent<Text>().text = $"Up to {limit} Characters";
        w.transform.SetAsLastSibling();
    }
    public void ReShow()
    {
        StartCoroutine(Co_ReShow());
    }
    IEnumerator Co_ReShow()
    {
        yield return null;
        Common_InputField.i.w.SetActive(true);
    }



    public void Btn_Ok()
    {
        _newValue = w.transform.Find("InputField").GetComponent<InputField>().text;
        w.transform.Find("InputField").GetComponent<InputField>().text = "";
        _newValue = _newValue.Trim();

        if (string.IsNullOrEmpty(_newValue) == false)
        {
            cb(_newValue);
        }
        else
        {
            ToastGroup.Show("EmptyInput".L());
            return;
        }
    }

    public void Btn_Cancel()
    {
        w.transform.Find("InputField").GetComponent<InputField>().text = "";
        UM.i.showings.Remove(w);
        Hide();
    }

    public void Hide()
    {
        UM.i.showings.Remove(w);
        w.SetActive(false);
    }
}
