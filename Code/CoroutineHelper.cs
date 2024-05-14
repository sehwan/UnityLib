using System;
using System.Collections;
using UnityEngine;


public class CoroutineHelper : MonoSingleton<CoroutineHelper>
{
    public void CallWaitForOneFrame(Action act)
    {
        i.StartCoroutine(DoCallWaitForOneFrame(act));
    }
    public void CallWaitForSeconds(float wait, Action act)
    {
        i.StartCoroutine(DoCallWaitForSeconds(wait, act));
    }
    private IEnumerator DoCallWaitForOneFrame(Action act)
    {
        yield return 0;
        act();
    }
    private IEnumerator DoCallWaitForSeconds(float seconds, Action act)
    {
        yield return new WaitForSeconds(seconds);
        act();
    }
}