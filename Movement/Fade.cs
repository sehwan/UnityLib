using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Fade : MonoSingleton<Fade>
{
    public GameObject go;
    public Image image;
    public Action onEnable;
    public Action onDisable;


    new void Awake()
    {
        base.Awake();
        MakeGameObject();
        // SceneManager.activeSceneChanged += (Scene s, Scene n) => { MakeGameObject(); };
    }
    void OnEnable()
    {
        if (onEnable != null) onEnable();
    }
    void OnDisable()
    {
        if (onDisable != null) onDisable();
    }

    void MakeGameObject()
    {
        go = new GameObject("fade");
        go.transform.parent = GameObject.Find("UM").transform;
        go.AddComponent<RectTransform>().sizeDelta = Def.RESOULUTION;
        go.GetComponent<RectTransform>().localPosition = Vector3.zero;
        image = go.AddComponent<Image>();
        go.SetActive(false);
    }


    // Only Dim
    public void Dim(Color color)
    {
        go.SetActive(true);
        go.transform.SetAsLastSibling();
        image.DOKill();
        image.color = color;
    }

    // 0 -> 1
    public void FadeIn(Color color, float time = 1.5f)
    {
        go.SetActive(true);
        go.transform.SetAsLastSibling();
        image.color = new Color(color.r, color.g, color.b, 0);
        image.DGCrossFadeAlpha(1, time);
    }

    // 1 -> 0
    public void FadeOut(Color color, float time = 1f, float delay = 0f)
    {
        go.SetActive(true);
        go.transform.SetAsLastSibling();
        image.color = new Color(color.r, color.g, color.b, 1);
        image.DGCrossFadeAlpha(0, time).SetDelay(delay).OnComplete(() => { go.SetActive(false); });
    }

    public void Hide()
    {
        go.SetActive(false);
    }
}
