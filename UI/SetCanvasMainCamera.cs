using UnityEngine;

public class SetCanvasMainCamera : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;        
    }
}
