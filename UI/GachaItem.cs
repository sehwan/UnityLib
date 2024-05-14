using System;
using UnityEngine;
using DG.Tweening;

public class GachaItem : MonoBehaviour
{
    public ItemSlot slot;
    public Transform screen;
    public GameObject vfx;


    void OnEnable()
    {
        if (vfx != null) vfx.SetActive(true);
        if (screen != null) screen.SetActive(false);
        // screen.SetActive(true);
        // vfx.SetActive(false);
        // SFX.Play("gachaShow");
    }
    public void OnClick()
    {
        if (vfx != null) vfx.SetActive(true);
        if (screen != null)
        {
            screen.SetActive(false);
            // if (screen.gameObject.activeInHierarchy) SFX.Play("gachaOpen");
        }
    }
}