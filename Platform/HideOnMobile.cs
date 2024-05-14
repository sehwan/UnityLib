using UnityEngine;

public class HideOnMobile : MonoBehaviour
{
    void Awake()
    {
        if (Application.isMobilePlatform) gameObject.SetActive(false);
    }
}