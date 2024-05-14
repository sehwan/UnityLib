using UnityEngine;
using UnityEngine.UI;

public class ImageOffsetScroller : MonoBehaviour
{
    [Header("The sprite MUST NOT be Atlased", order = 0)]
    [Space(2, order = 1)]
    public Vector2 DIR;

    Material mat;
    void Start()
    {
        mat = GetComponent<Image>().materialForRendering;
        // mat = GetComponent<Image>().material;
    }

    void Update()
    {
        // mat.mainTextureOffset = DIR * Time.time;
        mat.SetTextureOffset("_MainTex", DIR * Time.time);
    }
}
