using UnityEngine;
using System.Collections;

public class AutoDestroyAnimation : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, GetComponent<Animation>().clip.length);
    }
}