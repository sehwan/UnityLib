using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnPC : MonoBehaviour
{
    void Start()
    {
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
        Destroy(gameObject);
#endif
    }
}
