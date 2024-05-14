using UnityEngine;

public class HideByPlatform : MonoBehaviour
{
    public bool hideInPC = true;
    public bool hideInMobile;

    void Awake()
    {
        if (Application.isMobilePlatform && hideInMobile) gameObject.SetActive(false);
        else if (Application.isMobilePlatform == false && hideInPC) gameObject.SetActive(false);
    }
}
