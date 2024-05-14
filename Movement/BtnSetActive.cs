using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSetActive : MonoBehaviour
{
    public void Btn_Show(GameObject go = null)
    {
        if (go != null) go.SetActive(true);
        else gameObject.SetActive(true);
    }

    public void Btn_Hide(GameObject go = null)
    {
        if (go != null) go.SetActive(false);
        else gameObject.SetActive(false);
    }
}
