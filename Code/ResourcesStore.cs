using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesStore
{
    // sprite
    // public static Dictionary<string, Sprite> sprites = new();
    public static Sprite LoadSprite(this string key)
    {
        return Resources.Load<Sprite>(key);
        // if (sprites.ContainsKey(key) == false)
        // {
        //     var load = Resources.Load<Sprite>(key);
        //     if (load == null)
        //     {
        //         Debug.Log($"<color=orange>No File '{key}'</color>");
        //         return null;
        //     }
        //     sprites.Add(key, load);
        // }
        // return sprites[key];
    }

    // prefab
    // public static Dictionary<string, GameObject> prefabs = new();
    public static GameObject LoadGameObject(this string key)
    {
        return Resources.Load<GameObject>(key);
        // if (prefabs.ContainsKey(key) == false) prefabs.Add(key, Resources.Load<GameObject>(key));
        // return prefabs[key];
    }
}
