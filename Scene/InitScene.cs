using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InitScene : MonoBehaviour
{
    public Image logo;


    void Awake()
    {
        logo.CrossFadeAlpha(0, 0, true);
    }

    IEnumerator Start()
    {
        while (true)
        {
            logo.CrossFadeAlpha(1, 2, true);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("title");
        }
    }
}
