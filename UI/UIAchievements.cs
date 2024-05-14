using System.Collections.Generic;
using UnityEngine;


public class UIAchievements : UIWindow
{
    public GameObject prefab;
    public Transform tr_contents;
    public Dictionary<string, AchievementItem> items = new();



    // public override void Init()
    // {
    //     base.Init();
    //     //Clear
    //     foreach (Transform item in tr_contents)
    //     {
    //         Destroy(item.gameObject);
    //     }
    //     //Instantiate
    //     foreach (var item in GameData.meta.achv)
    //     {
    //         GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity, tr_contents);
    //         items.Add(item.Key, go.GetComponent<UIAchievementItem>());
    //     }
    //     //Check
    //     Refresh();
    // }

    // public override void Show()
    // {
    //     base.Show();
    //     Refresh();
    // }

    // public void Refresh()
    // {
    //     foreach (var item in items)
    //     {
    //         item.Value.Refresh(item.Key);
    //     }
    // }
}
