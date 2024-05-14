using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class ClickByKey : MonoBehaviour
{
    public KeyCode key = KeyCode.Space;
    public Button button;


    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            gameObject.SetActive(false);
            return;
        }
        GetComponent<Text>().text = $"[{KeyCodeToString(key)}]";
    }
    string KeyCodeToString(KeyCode k)
    {
        if (k > KeyCode.Alpha0 && k < KeyCode.Alpha9)
        {
            var n = (int)k - (int)KeyCode.Alpha0;
            return n.ToString();
        }
        if (k == KeyCode.Escape) return "ESC";
        return k.ToString().ToUpper();
    }

    void Update()
    {
        if (button.IsInteractable() == false) return;
        if (Input.GetKeyDown(key) == false) return;
        var um = UM.i;
        if (um.showings.Count > 0)
        {
            if (transform.IsChildOf(um.showings.Last().transform) == false)
                return;
        }
        else if (transform.IsChildOf(um.scenes.Last().transform) == false) return;
        if (um.others.Count > 0) return;

        // button?.onClick?.Invoke();
        ExecuteEvents.Execute(button.gameObject,
            new PointerEventData(EventSystem.current),
            ExecuteEvents.pointerClickHandler);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ClickByKey))]
public class ClickByKeEditor : Editor
{
    // void OnDisable()
    // {
    //     var t = target as ClickByKey;
    //     t.GetComponent<Text>().text = $"[{KeyCodeToString(t.key)}]";
    // }
    void OnEnable()
    {
        var t = target as ClickByKey;
        t.GetComponent<Text>().text = $"[{KeyCodeToString(t.key)}]";
    }

    string KeyCodeToString(KeyCode k)
    {
        if (k > KeyCode.Alpha0 && k < KeyCode.Alpha9)
        {
            var n = (int)k - (int)KeyCode.Alpha0;
            return n.ToString();
        }
        if (k == KeyCode.Escape) return "ESC";
        return k.ToString().ToUpper();
    }
}
#endif