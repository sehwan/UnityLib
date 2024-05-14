using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTimer : MonoBehaviour
{
    public float TIME = 3f;

    void OnEnable()
    {
        this.InvokeEx(() => gameObject.SetActive(false), TIME);
    }
}
