using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualMouseManager : MonoBehaviour
{
    RectTransform rect;
    public float speed = 5f;
    public GameObject virtualCursor;
    bool wasGamepadActive = false;
    int screenWidth, screenHeight;


    void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.zero;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        rect.anchoredPosition = new Vector2(screenWidth / 2f, screenHeight / 2f);
        SetActiveVirtualCursor(false);
    }

    void Update()
    {
        bool gamepadActive = false;
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Vector2 stick = gamepad.leftStick.ReadValue();
            Debug.Log("Gamepad Stick: " + stick);
            if (stick.sqrMagnitude > 0.0001f)
            {
                Vector2 newPos = rect.anchoredPosition + speed * Time.deltaTime * stick;
                rect.anchoredPosition = new Vector2(
                    Mathf.Clamp(newPos.x, 0, screenWidth),
                    Mathf.Clamp(newPos.y, 0, screenHeight)
                );
                gamepadActive = true;
            }
        }

        if (gamepadActive != wasGamepadActive)
        {
            if (gamepadActive) SetActiveVirtualCursor(true);
            else SetActiveVirtualCursor(false);
        }
        wasGamepadActive = gamepadActive;
    }

    void SetActiveVirtualCursor(bool active)
    {
        virtualCursor.SetActive(active);
        Cursor.visible = active == false;
    }
}
