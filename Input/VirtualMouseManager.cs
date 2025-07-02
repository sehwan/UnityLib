using UnityEngine;

public class VirtualMouseManager : MonoBehaviour
{
    [SerializeField] RectTransform virtualCursor;
    
    Vector2 screenBounds;
    Vector2 cachedPosition;

    void Awake()
    {
        screenBounds = new Vector2(Screen.width, Screen.height);
    }

    void LateUpdate()
    {
        ClampCursorToScreen();
    }


    void ClampCursorToScreen()
    {
        cachedPosition = virtualCursor.anchoredPosition;
        cachedPosition.x = Mathf.Clamp(cachedPosition.x, 0, screenBounds.x);
        cachedPosition.y = Mathf.Clamp(cachedPosition.y, 0, screenBounds.y);
        virtualCursor.anchoredPosition = cachedPosition;
    }
}