using UnityEngine;
using UnityEngine.UI;
using System;


public class TermPanel : MonoBehaviour
{
    public static bool CheckTerm()
    {
        if (PlayerPrefs.GetInt("term", 0) == 0
            && Application.systemLanguage == SystemLanguage.Korean
        ) return false;
        else return true;
    }


    public Toggle toggle;
    public Button btn_ok;
    Action onOK;


    public static TermPanel Instantiate()
    {
        return ((GameObject)Instantiate(Resources.Load("Prefabs/term_pn"))).GetComponent<TermPanel>();
    }

    public void Show(Action cb)
    {
        gameObject.SetActive(true);
        btn_ok.interactable = false;
        toggle.isOn = false;
        onOK = cb;
    }
    public void Btn_Ok()
    {
        PlayerPrefs.SetInt("term", 1);
        if (onOK != null) onOK();
        Destroy(gameObject);
    }

    public void OnToggle()
    {
        btn_ok.interactable = toggle.isOn;
    }

    public void ShowTerm()
    {
        Application.OpenURL("https://sites.google.com/view/alchemists-term/");
    }
}
