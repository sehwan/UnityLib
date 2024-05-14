using UnityEngine;
using System.Collections;

// public class MyWebImageView : UniWebView
public class MyWebImageView : MonoBehaviour
{
    public void Close()
    {
        Destroy(gameObject);
    }
}
