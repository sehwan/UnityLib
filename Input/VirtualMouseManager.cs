using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualMouseManager : MonoBehaviour
{
    [SerializeField] RectTransform virtualCursor;

    Vector2 screenBounds;
    bool wasGamepadActive = false;
    Vector2 lastMousePosition;


    void Awake()
    {
        virtualCursor.SetActive(false);
        screenBounds = new Vector2(Screen.width, Screen.height);
        virtualCursor.anchoredPosition = new Vector2(screenBounds.x / 2f, screenBounds.y / 2f);
        lastMousePosition = Input.mousePosition;
    }

    void SetActiveVirtualCursor(bool active)
    {
        virtualCursor.SetActive(active);
        Cursor.visible = active == false;
    }


    void Update()
    {
        // 마우스 움직임 감지
        Vector2 currentMousePosition = Input.mousePosition;
        bool mouseMoving = Vector2.Distance(currentMousePosition, lastMousePosition) > 0.1f;

        // 게임패드 입력 감지
        bool gamepadInput = false;
        if (Gamepad.current != null)
        {
            gamepadInput = Gamepad.current.leftStick.ReadValue().magnitude > 0.1f ||
                          Gamepad.current.rightStick.ReadValue().magnitude > 0.1f ||
                          Gamepad.current.dpad.ReadValue().magnitude > 0.1f;
        }

        // 입력에 따라 가상 커서 토글
        if (mouseMoving)
        {
            SetActiveVirtualCursor(false);
            wasGamepadActive = false;
        }
        else if (gamepadInput && !wasGamepadActive)
        {
            SetActiveVirtualCursor(true);
            wasGamepadActive = true;
            virtualCursor.anchoredPosition = currentMousePosition;
        }

        lastMousePosition = currentMousePosition;
    }

    void LateUpdate()
    {
        // Clamp
        Vector2 pos = virtualCursor.anchoredPosition;
        virtualCursor.anchoredPosition = new Vector2(
            Mathf.Clamp(pos.x, 0, screenBounds.x),
            Mathf.Clamp(pos.y, 0, screenBounds.y)
        );
    }
}