using UnityEngine;

public class ShotSFXOnEnable : MonoBehaviour
{
    public string clip;
    void OnEnable()
    {
        SFX.Play(clip);
    }
}
