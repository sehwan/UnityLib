using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Text))]
public class ClickByKey : MonoBehaviour
{
    Transform tr;
    public KeyCode key = KeyCode.Space;
    public Button button;
    static UM um;


    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            Destroy(this);
            return;
        }
        tr = transform;
        GetComponent<Text>().text = $"[{KeyCodeToString(key)}]";
    }
    void Start()
    {
        if (um == null) um = UM.i;
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
        if (um.others.Count > 0) return;
        if (um.showings.Count > 0 &&
            tr.IsChildOf(um.showings.Last().transform) == false) return;
        if (um.scenes.Count > 0 &&
            tr.IsChildOf(um.scenes.Last().transform) == false) return;

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