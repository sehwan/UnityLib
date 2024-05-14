using UnityEngine;
using System.Collections;

// public class MyUniWebView : UniWebView
public class MyUniWebView : MonoBehaviour
{
    public void Close()
    {
        // WebViewDone("");
        Destroy(gameObject);
    }
}
