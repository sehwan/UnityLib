using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Text))]
public class ClickByKey : MonoBehaviour
{
    Transform tr;
    public bool isNumber123 = false;
    public KeyCode key = KeyCode.Space;
    public Button button;
    static UM um;


    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            Destroy(gameObject);
            return;
        }
        tr = transform;
        GetComponent<Text>().text = $"[{KeyCodeToString()}]";
    }
    void Start()
    {
        if (um == null) um = UM.i;
    }
    public string KeyCodeToString()
    {
        if (isNumber123)
            key = KeyCode.Alpha1 + button.transform.GetSiblingIndex();
        if (key > KeyCode.Alpha0 && key < KeyCode.Alpha9)
        {
            var n = (int)key - (int)KeyCode.Alpha0;
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
        return key.ToString().ToUpper();
    }

    void Update()
    {
        if (button.IsInteractable() == false) return;
        if (Input.GetKeyDown(key) == false) return;
        if (um.others.Count > 0) return;
        // if (um.showings.Count > 0 &&
        //     tr.IsChildOf(um.showings.Last().transform) == false) return;
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
    void OnEnable()
    {
        var t = target as ClickByKey;
        t.GetComponent<Text>().text = $"[{t.KeyCodeToString()}]";
    }
}
#endif