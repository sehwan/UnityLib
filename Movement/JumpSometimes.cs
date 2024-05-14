using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumpSometimes : MonoBehaviour
{
    [Header("Jump")]
    public float power = 6;
    public int number = 1;
    public float duration = 0.2f;

    [Header("Invoke")]
    public float delayMin = 1f;
    public float delayMax = 2f;
    public float interval = 3f;

    public Vector3 originPos;

    void OnEnable()
    {
        originPos = transform.localPosition;
        this.InvokeRepeatingEx(Jump, RandomEx.R(delayMax, delayMin), interval);
    }
    void OnDisable()
    {
        CancelInvoke();
    }

    void Jump()
    {
        transform.DOLocalJump(originPos, power, number, duration);
    }
}
