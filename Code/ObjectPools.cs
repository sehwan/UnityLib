using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class ObjectPools : MonoSingleton<ObjectPools>
{
    [Serializable]
    public class ObjectPool
    {
        public int iter;
        public GameObject[] gos;
        public ObjectPool(GameObject[] gos)
        {
            this.gos = gos;
        }
    }

    public SerializedDictionary<string, ObjectPool> _pools = new();
    public static SerializedDictionary<string, ObjectPool> pools
    {
        get => i._pools;
    }
    public static void Clear()
    {
        foreach (var item in pools)
            foreach (var go in item.Value.gos) GM.Destroy(go);
        pools.Clear();
    }
    // Make a Pool
    public static void Make(string key, string prefabPath, int size, Transform parent)
    {
        var prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab == null)
        {
            Debug.LogError($"No Prefab in '{prefabPath}'");
            return;
        }
        Make(key, prefab, size, parent);
    }
    public static void Make(string key, GameObject original, int size, Transform parent)
    {
        var originalSize = 0;
        if (pools.ContainsKey(key))
        {
            originalSize = pools[key].gos.Length;
            Array.Resize(ref pools[key].gos, originalSize + size);
        }
        else pools.Add(key, new ObjectPool(new GameObject[size]));

        original.SetActive(false);
        var length = pools[key].gos.Length;
        for (int i = originalSize; i < length; i++)
        {
            var go = GM.Instantiate(original, Vector2.zero, Quaternion.identity, parent);
            go.transform.parent = parent;
            pools[key].gos[i] = go;
            go.name = $"{key}_{i}";
        }
    }

    const int COUNT_per_ADD = 5;
    public static GameObject Get(string key)
    {
        var pool = pools[key];
        var gos = pool.gos;
        var cur = gos[pool.iter];

        // it's still On. so it's Not Enough
        // if (cur == null) Debug.Log($"<color=red>{key}:{pool.iter}/{gos.Length - 1} is null</color>");
        if (cur.activeSelf)
        {
            Array.Resize(ref gos, gos.Length + COUNT_per_ADD);
            pool.gos = gos;
            // Make Clones
            for (int i = gos.Length - COUNT_per_ADD; i < gos.Length; i++)
            {
                gos[i] = GM.Instantiate(cur, Vector2.zero, cur.transform.rotation, cur.transform.parent);
                gos[i].transform.SetSiblingIndex(cur.transform.GetSiblingIndex() + 1 + i);
                gos[i].SetActive(false);
                gos[i].name = $"{key}_{i}";
            }
            pool.iter = gos.Length - COUNT_per_ADD;
            cur = gos[pool.iter];
        }
        cur.SetActive(true);
        pool.iter++;
        if (pool.iter >= gos.Length) pool.iter = 0;
        return cur;
    }
}
