using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserResourceText : MonoBehaviour
{
    public string key;
    public Text text;
    public Image icon;

    public static object data;

    void Awake()
    {
        text.text = "";
    }

    void OnEnable()
    {
        if (icon != null) icon.sprite = UIUtil.GetIcon(key);
    }

    void Update()
    {
        var rsc = data.GetField<int>(key);
        text.text = $"{rsc:n0}";
        text.color = rsc > 0 ?
            Color.white :
            Color.red;
    }
}
