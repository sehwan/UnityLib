using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAnimationLoopImage : MonoBehaviour
{
    public Image ren;
    public Sprite[] sprites;
    public float delay = 0.25f;

    void OnEnable()
    {
        StartCoroutine(Co_Animate());
    }

    public void SetSprites(Sprite[] sprites, float delay)
    {
        this.sprites = sprites;
        this.delay = delay;
    }
    public IEnumerator Co_Animate()
    {
        var w = CoroutineEx.GetWait(delay);
        while (true)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                ren.sprite = sprites[i];
                yield return w;
            }
        }
    }
}