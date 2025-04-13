using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Prologue : MonoBehaviour
{
    public Transform tr_seq;

    [TextArea(10, 25)]
    public string prologueText = "";


    public static bool HavePrologue()
    {
        return PlayerPrefs.GetInt("prolog", 0) == 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) PlayerPrefs.SetInt("prologue", 0);
        if (Input.GetMouseButton(0)) Time.timeScale = 15;
        else Time.timeScale = 1;
    }

    void Start()
    {
        if (HavePrologue())
        {
            NextScene();
            return;
        }
        StartCoroutine(Co_Prologue());
    }

    public IEnumerator Co_Prologue()
    {
        // Hide All
        tr_seq.SetActive(true);
        foreach (Transform e in tr_seq)
        {
            e.GetComponent<Graphic>().CrossFadeAlpha(0, 0, false);
        }

        // Show All
        var splitted = prologueText.Split('\n');
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < splitted.Length; i++)
        {
            if (splitted[i].IsNullOrEmpty()) continue;
            var text = tr_seq.GetChild(i).GetComponent<Text>();
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
        PlayerPrefs.SetInt("prolog", 1);
        Fade.i.Out(Color.black, 0.4f);
        Destroy(gameObject);
    }
}
