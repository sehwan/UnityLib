using System;
using System.Collections;
using UnityEngine;

public enum GameState { Quit, Pause, Playing }


public class GM : MonoSingleton<GM>
{
    public bool showTitle = false;
    public TitleScene title;
    public Action beforeInit;
    public Action onCloseTitle;
    public Action onInit;
    public Action onInit2;
    [Immutable] public GameState state;
    public string[] banObjects;


    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = Settings.FrameRate;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

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
        title?.gameObject.SetActive(showTitle);
        // Firebase
        // while (FirebaseMng.inst.didFetchConfig == false) yield return null;
        // while (FirebaseMng.inst.user == null) yield return null;
        // while (GameData.i.didInitMeta == false) yield return null;
        User.i.LoadOrNew();
        // while (User.WasInit == false) yield return null;
        // GameData.i.FetchAfterUser();
        // while (GameData._inst.didFetchedRunningMeta == false) yield return null;
        // After Meta
        // Purchaser.inst.Init();

        // Term
        // if (TermPanel.CheckTerm() == false)
        //     TermPanel.Instantiate().Show(() => PlayerPrefs.SetInt("term", 1));
        // while (TermPanel.CheckTerm() == false) yield return null;

        yield return null;
        beforeInit?.Invoke();
        while (title != null && title.gameObject.activeSelf) yield return null;
        onCloseTitle?.Invoke();

        // Start World
        // ActivateWithLevel.Refresh_Tut();
        // ActivateWithLevel.Refresh_Stage();
        onInit?.Invoke();
        onInit2?.Invoke();

        // TimeManager.i.AddTimer(1, () =>
        // {
        //     var u = User.data;
        //     u.time++;
        //     u.time_m++;
        //     User.i.dt_lastFocused = DateTime.Now;
        // });

        // User.data.tester = true;
        // Guide Quest
        // GuideQuest.I.Scenario();

        // Tutorial
        // User.data.tut = 0; // For Test
        // if (User.data.IsTutorialOver() == false && User.inst.isSkipTutorial == false)
        // {
        //     TutorialMng.Play();
        //     while (User.data.IsTutorialOver() == false) yield return null;
        // }

    }

    public void SetState(GameState n)
    {
        state = n;
    }
}
