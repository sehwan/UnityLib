using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UM : MonoBehaviour
{
    public static UM i;
    public Canvas canvas;
    public Camera cam;
    public List<UIWindow> showings = new();
    public List<GameObject> others = new();
    public List<UIWindow> windows = new();
    public List<UIScene> scenes = new();
    public ParticleSystem touchFX;
    public Texture2D cursor;
    public Material mat_gray;


    void Awake()
    {
        i = this;
        canvas = GetComponent<Canvas>();

        // Registers all windows
        windows = FindObjectsOfType<UIWindow>(true).ToList();
        scenes = FindObjectsOfType<UIScene>(true).ToList();

        // if (Application.isMobilePlatform == false)
        // {
        //     Cursor.visible = true;
        //     Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
        // }
    }
    void Start()
    {
        HideWindows();
        GM.i.beforeInit += InitAll;
    }
    void InitAll()
    {
        scenes.ForEach(e => e.Init());
        windows.ForEach(e => e.Init());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) MakeTouchEffect();
    }
    void MakeTouchEffect()
    {
        Vibration.Vibrate(1);
        if (Time.timeScale < 1) return;
        if (touchFX == null) return;
        touchFX.Emit(cam.ScreenToWorldPoint(Input.mousePosition));
    }

    public static T Get<T>() where T : UIWindow
    {
        return i.windows.Find(e => e is T) as T;
    }


    // Managing
    public Action<GameObject> onRegisterWindow;
    public Action<GameObject> onDeregisterWindow;
    public void RegisterWindow(UIWindow w)
    {
        if (showings.Contains(w) == false)
        {
            showings.Add(w);
        }
        w.transform.SetAsLastSibling();
        onRegisterWindow?.Invoke(w.gameObject);
    }
    public void DeregisterWindow(UIWindow w)
    {
        showings.Remove(w);
        onDeregisterWindow?.Invoke(w.gameObject);
    }


    public void HideWindows()
    {
        windows.Where(e => e.IsActive).ToList().ForEach(e => e.Hide());
    }
    public void HideScenes()
    {
        foreach (var e in scenes) e.Hide();
    }
}