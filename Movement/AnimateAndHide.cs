using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimateAndHide : MonoBehaviour
{
    SpriteRenderer ren;
    public Sprite[] sprites;
    public float delay = 0.25f;


    void Awake()
    {
        ren = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        StartCoroutine(Co_Animate());
    }
    IEnumerator Co_Animate()
    {
        var w = new WaitForSeconds(delay);
        for (int i = 0; i < sprites.Length; i++)
        {
            ren.sprite = sprites[i];
            yield return w;
        }
        gameObject.SetActive(false);
    }
}
