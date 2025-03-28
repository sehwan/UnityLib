using UnityEngine;
using UnityEngine.UI;

public class OnEnableFadeIn : MonoBehaviour
{
    public MaskableGraphic graphic;
    public float dur;
    public float maxAlpha;

    void Onable()
    {
        graphic.SetAlpha(0);
        graphic.CrossFadeAlpha(maxAlpha, dur, false);
    }
}
