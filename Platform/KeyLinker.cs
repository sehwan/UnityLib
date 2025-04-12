using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;

[RequireComponent(typeof(Text))]
public class KeyLinker : MonoBehaviour
{
    Transform tr;
    public KeyCode key = KeyCode.Space;
    public PointerDown pointerDown;
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
        if (key == KeyCode.Space) return "␣";
        if (key == KeyCode.Escape) return "ESC";
        if (key == KeyCode.LeftArrow) return "←";
        if (key == KeyCode.RightArrow) return "→";
        if (key == KeyCode.UpArrow) return "↑";
        if (key == KeyCode.DownArrow) return "↓";
        if (key == KeyCode.LeftShift) return "L SHIFT";
        if (key == KeyCode.RightShift) return "R SHIFT";
        return k.ToString().ToUpper();
    }

    void Update()
    {
        if (Input.GetKey(key) == false) return;
        if (pointerDown.enabled == false) return;
        if (um.others.Count > 0) return;
        if (um.showings.Count > 0 &&
            tr.IsChildOf(um.showings.Last().transform) == false) return;
        if (um.scenes.Count > 0 &&
            tr.IsChildOf(um.scenes.Last().transform) == false) return;

        pointerDown.cb.Invoke();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(KeyLinker))]
public class KeyLinkerEditor : Editor
{
    void OnEnable()
    {
        var t = target as KeyLinker;
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