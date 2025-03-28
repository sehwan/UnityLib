using UnityEngine;

public class CanvasAutoMainCameraAssigner : MonoBehaviour
{
    public void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas.renderMode != RenderMode.WorldSpace && canvas.worldCamera == null)
            canvas.worldCamera = Camera.main;
    }
}