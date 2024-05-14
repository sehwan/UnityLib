using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWithLevel : MonoBehaviour
{
    public int unlockTut;
    public static List<ActivateWithLevel> tut = new();
    public static void Refresh_Tut()
    {
        var level = User.data.tut;
        if (level < 0) level = int.MaxValue;
        foreach (var item in tut)
            item.gameObject.SetActive(item.unlockTut <= level);
        tut.RemoveAll(x => x.unlockTut <= level);
    }

    public int unlockStage;
    public static List<ActivateWithLevel> stage = new();
    public static void Refresh_Stage()
    {
        var level = User.data.stageHigh;
        foreach (var item in stage)
            item.gameObject.SetActive(item.unlockStage <= level);
        stage.RemoveAll(x => x.unlockStage <= level);
    }

    
    void Start()
    {
        gameObject.SetActive(false);
        if (unlockTut != 0)
        {
            tut.Add(this);
            if (User.data.IsFilled())
            {
                var lv = User.data.tut;
                if (lv < 0) lv = int.MaxValue;
                gameObject.SetActive(unlockTut <= lv);
                // tut.Remove(this);
            }
        }
        if (unlockStage != 0)
        {
            stage.Add(this);
            if (User.data.IsFilled())
            {
                gameObject.SetActive(unlockStage <= User.data.stageHigh);
                // stage.Remove(this);
            }
        }
    }
}
