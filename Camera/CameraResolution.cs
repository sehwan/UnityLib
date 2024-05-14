using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public int w = 9, h = 16;

    [ContextMenu("Adjust")]
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        Debug.Log($"<color=cyan>{Screen.width} {Screen.height} {(float)Screen.width / Screen.height}</color>");
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)w / h);
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect;
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}
