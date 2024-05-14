using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Toasts : MonoSingleton<Toasts>
{
    static float pos_y = 0.25f;
    public GameObject[] gos = new GameObject[9];
    public int idx;
    public int count;



    new void Awake()
    {
        base.Awake();
        OnSceneChanged();
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            OnSceneChanged();
        };
    }
    void OnSceneChanged()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/toast");
        Transform canvas = GameObject.Find("UM").transform;
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i] = Instantiate(prefab, Vector3.zero, Quaternion.identity, canvas);
            gos[i].SetActive(false);
        }
    }

    public void Show(string s)
    {
        Debug.Log("toast : " + s);
        StartCoroutine(Co_Show(s));
    }
    IEnumerator Co_Show(string s)
    {
        //delay
        yield return new WaitForSeconds(count * 0.5f);
        count++;

        //go
        GameObject go = gos[idx];
        go.SetActive(true);
        go.transform.SetAsLastSibling();
        go.transform.localPosition = new Vector2(0, Def.RESOULUTION.y * pos_y);
        //text
        Text txt = go.Finds("text").GetComponent<Text>();
        txt.CrossFadeAlpha(0.85f, 0.1f, true);
        txt.text = s;
        RectTransform r = txt.GetComponent<RectTransform>();
        //image
        Image img = go.GetComponent<Image>();
        img.CrossFadeAlpha(0.85f, 0.1f, true);
        //break line
        txt.text = s.BreakLine(40);
        //animate
        var dest = Def.RESOULUTION.y * (pos_y + 0.08f);
        var time = 2.5f;
        go.transform.DOLocalMoveY(dest, time).OnComplete(() =>
        {
            img.CrossFadeAlpha(0, 0.44f, true);
            txt.CrossFadeAlpha(0, 0.4f, true);
        });
        //indexing
        idx++;
        if (gos.Length <= idx) idx = 0;
        //hide
        yield return new WaitForSeconds(time + 0.1f);
        count--;
        go.SetActive(false);
    }
}