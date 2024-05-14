using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TimerPauser : MonoBehaviour
{
    void OnEnable()
    {
        TimeManager.i.AddTimeScaler(gameObject, 0);
    }
    void OnDisable()
    {
        TimeManager.i.RemoveTimeScaler(gameObject);
    }
}
