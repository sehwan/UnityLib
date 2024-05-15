using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BugRummager : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Co_Rummage());
    }
    IEnumerator Co_Rummage()
    {
        Debug.Log("Rummaging...");
        Vector2 pos;
        while (true)
        {
            yield return null;
            pos = new Vector2(RandomEx.R(Screen.width), RandomEx.R(Screen.height));
            // um.touchFX.Emit(um.cam.ScreenToWorldPoint(pos).Z0());
            // UI
            if (SimulateClickOnUI(pos)) continue;
            // Down Object
            yield return null;
            if (SimulateClick2D(pos, _OnDown)) continue;
            yield return null;
            if (SimulateClick3D(pos, _OnDown)) continue;
            // Up Object
            pos = new Vector2(RandomEx.R(Screen.width), RandomEx.R(Screen.height));
            yield return null;
            if (SimulateClick2D(pos, _OnUp)) continue;
            yield return null;
            if (SimulateClick3D(pos, _OnUp)) continue;
        }
    }

    bool SimulateClickOnUI(Vector2 pos)
    {
        PointerEventData pointerData = new(EventSystem.current)
        {
            position = pos
        };
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(pointerData, results);
        if (results.Count == 0) return false;
        ExecuteEvents.Execute(results[0].gameObject, pointerData, ExecuteEvents.pointerClickHandler);
        return true;
    }

    static string _OnDown = "OnMouseDown";
    static string _OnUp = "OnMouseUp";
    bool SimulateClick3D(Vector3 pos, string msg)
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(pos);
        var hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider == null) return false;
        hit.transform.gameObject.SendMessage(msg, SendMessageOptions.DontRequireReceiver);
        return true;
    }
    bool SimulateClick2D(Vector3 pos, string msg)
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(pos);
        var hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider == null) return false;
        hit.transform.gameObject.SendMessage(msg, SendMessageOptions.DontRequireReceiver);
        return true;
    }
}
