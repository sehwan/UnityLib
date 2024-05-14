using UnityEngine;

public class ScaleOnPC : MonoBehaviour
{
    [Range(0, 1)]
    public float pcX = 1;
    [Range(0, 1)]
    public float pcY = 1;

    void Awake()
    {
        if (Application.isMobilePlatform == false)
        {
            var rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x * pcX, rt.sizeDelta.y * pcY);
        }
    }
}
