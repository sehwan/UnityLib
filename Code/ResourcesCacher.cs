using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 리소스 캐싱 매니저.
///</summary>
public class ResourcesCacher : MonoSingleton<ResourcesCacher>
{
    Dictionary<string, Object> caches = new();


    public T Load<T>(string path) where T : Object
    {
        if (caches.ContainsKey(path) == false)
        {
            caches[path] = Resources.Load(path);
        }
        return caches[path] as T;
    }


    public void Clear()
    {
        caches.Clear();
        Resources.UnloadUnusedAssets();
    }


    public void GetKeys()
    {
        Debug.Log(string.Format("caches {0} times", caches.Count));
        foreach (var item in caches)
        {
            Debug.Log(item.Key);
        }
    }
}
