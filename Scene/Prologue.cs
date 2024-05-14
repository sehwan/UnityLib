using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class Prologue : MonoBehaviour
{
    public Transform prologue;
    public string nextSceneName = "stage";

    [TextArea(10, 25)]
    public string prologueText = "";


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) PlayerPrefs.SetInt("prologue", 0);
        if (Input.GetMouseButton(0)) Time.timeScale = 15;
        else Time.timeScale = 1;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("prologue", 0) == 1)
        {
            NextScene();
            return;
        }
        StartCoroutine(Co_Prologue());
    }

    IEnumerator Co_Prologue()
    {
        // Hide All
        prologue.SetActive(true);
        foreach (Transform item in prologue)
        {
            item.GetComponent<Graphic>().CrossFadeAlpha(0, 0, false);
        }

        // Show All
        var splitted = prologueText.Split('\n');
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < splitted.Length; i++)
        {
            if (splitted[i].IsNullOrEmpty()) continue;
            var text = prologue.GetChild(i).GetComponent<Text>();
            text.text = splitted[i];
            text.CrossFadeAlpha(1, 3, false);
            yield return new WaitForSeconds(3f);
        }
        yield return new WaitForSeconds(1.5f);
        NextScene();
    }

    void NextScene()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("prologue", 1);
        SceneManager.LoadScene(nextSceneName);
    }
}
