using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Simple Image Scroller
///</summary>
public class OffsetScroller : MonoBehaviour
{
    public float _speed = 0.5f;
    public MeshRenderer _renderer;


    Vector2 offset;
    void Update()
    {
        offset = new Vector2(Time.time * _speed, 0);
        _renderer.material.mainTextureOffset = offset;
    }
}
