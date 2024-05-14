using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatText : MonoBehaviour
{
    public string[] texts;
    public float delay;



    void OnEnable()
    {
        StartCoroutine(Co_Repeat());
    }
    void OnDisenable()
    {
        StopAllCoroutines();
    }


    IEnumerator Co_Repeat()
    {
        while (true)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                GetComponent<Text>().text = texts[i];
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
