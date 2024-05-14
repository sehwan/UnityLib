using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimateAndSuicide : MonoBehaviour
{
    SpriteRenderer ren;
    public Sprite[] sprites;
    public float delay = 0.25f;
    public float life = 5f;

    void Awake()
    {
        ren = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        StartCoroutine(Co_Animate());
    }
    public void Init(float l)
    {
        life = l;
    }
    IEnumerator Co_Animate()
    {
        var w = new WaitForSeconds(delay);
        while (life > 0)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                ren.sprite = sprites[i];
                yield return w;
            }
        }
    }
}
