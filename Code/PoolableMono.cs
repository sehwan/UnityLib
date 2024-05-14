using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolableMono<T> : MonoBehaviour where T : PoolableMono<T>
{
    public static List<T> pool = new();
    public static int poolSize;


    public static void PoolPrefab(string prefabPath, int size, Transform parent)
    {
        poolSize = size;
        var target = prefabPath.LoadGameObject();
        for (int i = 0; i < size; i++)
        {
            var go = Instantiate(target, Vector2.zero, Quaternion.identity, parent);
            go.SetActive(false);
            pool.Add(go.GetComponent<T>());
        }
    }
    public static void Pool(int size, Transform parent)
    {
        poolSize = size;
        var target = new GameObject();
        var name = typeof(T).FullName;
        for (int i = 0; i < size; i++)
        {
            var go = Instantiate(target, Vector2.zero, Quaternion.identity, parent);
            go.name = name;
            go.SetActive(false);
            pool.Add(go.AddComponent<T>());
        }
    }


    public static void Foreach(Action<T> act)
    {
        pool.ForEach(e => act(e));
    }
    public static void HideAll()
    {
        pool.ForEach(e => e.gameObject.SetActive(false));
    }



    public static int iter_pool;
    public static T Get()
    {
        if (iter_pool >= poolSize) iter_pool = 0;
        var r = pool[iter_pool];
        iter_pool++;
        r.gameObject.SetActive(true);
        return r;
    }
    public static T GetLatest()
    {
        var latest = iter_pool - 1;
        if (iter_pool == 0) latest = poolSize - 1;
        return pool[latest];
    }
}
