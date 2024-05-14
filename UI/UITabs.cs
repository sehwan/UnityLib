using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UITab
{
    public GameObject[] objects = new GameObject[1];
}


public class UITabs : MonoBehaviour
{
    public int defaultShowingTab;
    public UITab[] tabs;
    public UnityEvent<int> onTabChanged;


    [ContextMenu("Tab To Default")]
    public void Start()
    {
        Tab(defaultShowingTab);
    }


    public void Tab(int order)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            foreach (var item in tabs[i].objects)
            {
                if (item == null)
                {
                    print($"null item {i}");
                    continue;
                }
                item.SetActive(i == order);
            }
        }
        onTabChanged?.Invoke(order);
    }


    public void TabBySiblingIndex(Transform tr)
    {
        Tab(tr.GetSiblingIndex());
    }
}
