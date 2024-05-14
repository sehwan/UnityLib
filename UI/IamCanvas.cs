using UnityEngine;

public class IamCanvas : MonoBehaviour
{
    public static IamCanvas inst;
    
    void Awake()
    {
        inst = this;
    }
}
