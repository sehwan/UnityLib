using UnityEngine;

public class SetInactiveInEditor : MonoBehaviour
{
    void Start()
    {
        if (Application.isEditor) gameObject.SetActive(false);
    }
}
