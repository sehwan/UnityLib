using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UserResourceText : MonoBehaviour
{
    public string key;
    public Text text;
    public Image icon;

    FieldInfo fieldInfo;
    int lastRsc;

    void Awake()
    {
        text.text = "";
        fieldInfo = (typeof(UserData)).GetField(key);
        if (icon != null) icon.sprite = UIUtil.GetIcon(key);
    }

    void Update()
    {
        if (fieldInfo == null) return;
        var neo = (int)fieldInfo.GetValue(User.i._);
        if (neo != lastRsc)
        {
            text.text = $"{neo:n0}";
            text.color = neo > 0 ? Color.white : Color.red;
            lastRsc = neo;
        }
    }
}
