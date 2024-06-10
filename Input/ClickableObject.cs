using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    static Vector3 mousePos;
    const float CLICK_DISTANCE = 20f;

    void OnMouseDown()
    {
        mousePos = Input.mousePosition;
    }
    void OnMouseUp()
    {
        if (UnityEx.IsWrongClick()) return;
        if (Vector3.Distance(mousePos, Input.mousePosition) > CLICK_DISTANCE) return;
        SendMessage("OnClick");
    }
}