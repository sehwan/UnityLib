using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserBigNumberText : MonoBehaviour
{
    public string key;
    public Text text;
    public Image icon;


    void Awake()
    {
        text.text = null;
    }
    void OnEnable()
    {
        icon.sprite = UIUtil.GetIcon(key);
    }
    void Update()
    {
        if (User.IsFilled() == false) return;
        text.text = User.i._.GetField<BigNumber>(key).ToString();
    }
}
