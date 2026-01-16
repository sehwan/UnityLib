using System;
using System.Collections;
using UnityEngine;

public enum GameState { Quit, Pause, Playing }


public class GM : MonoSingleton<GM>
{
    [Range(0, 3)]
    public int skipLevel = 0;
    public TitleScene title;
    public Action beforeInit;
    public Action onCloseTitle;
    public Action onInit;
    public Action onInit2;
    [Immutable] public GameState state;


    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = Settings.FrameRate;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (Application.isEditor == false) skipLevel = 0;

        // Camera.main.orthographicSize = 22f / targetAspect;
        // size
        // float w = Screen.width;
        // float h = Screen.height;
        // Screen.SetResolution(720, (int)(h / w * 720), true, 60);
    }


    IEnumerator Start()
    {
        // var targetAspect = (float)Screen.width / Screen.height;
        // CameraWork.i.SetZoom(15f / targetAspect);

        // Fade.i.Dim(Color.black);
        // Title
        title?.gameObject.SetActive(skipLevel == 0);
        title?.OnInit();

        yield return null;
        beforeInit?.Invoke();
        while (title != null && title.gameObject.activeSelf) yield return null;
        onCloseTitle?.Invoke();

        onInit?.Invoke();
        onInit2?.Invoke();
    }

    public void SetState(GameState n)
    {
        state = n;
    }

}
