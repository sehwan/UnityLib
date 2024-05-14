using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public static class CollectionEx
{
    public static bool IsNullOrEmpty<T>(this ICollection<T> me)
    {
        return me == null || me.Count == 0;
    }
    public static int FindIndex<T>(this T[] arr, Predicate<T> pred)
    {
        return Array.FindIndex(arr, pred);
    }
    public static int IndexOf<T>(this T[] arr, T val)
    {
        return Array.IndexOf(arr, val);
    }

    public static T[] FindAll<T>(this T[] we, Predicate<T> pred)
    {
        return Array.FindAll(we, pred);
    }
    public static T Find<T>(this T[] me, Predicate<T> pred)
    {
        return Array.Find(me, pred);
    }

    public static bool Exists<T>(this T[] me, Predicate<T> pred)
    {
        return Array.Exists(me, pred);
    }

    public static List<T> RemoveLast<T>(this List<T> me)
    {
        me.RemoveAt(me.Count - 1);
        return me;
    }

    public static void ForEach<T>(this T[] arr, Action<T> cb)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            cb(arr[i]);
        }
    }
    public static void ForEach<T>(this Dictionary<string, T> dict, Action<T> cb)
    {
        foreach (var item in dict)
        {
            cb(item.Value);
        }
    }
    public static void ForEach<T>(this IEnumerable<KeyValuePair<string, T>> dict, Action<T> cb)
    {
        foreach (var item in dict)
            cb(item.Value);
    }
    public static void ForEachReversed<T>(this List<T> list, Action<T> cb)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            cb(list[i]);
        }
    }
    public static void ForEachReversed<T>(this T[] arr, Action<T> cb)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            cb(arr[i]);
        }
    }

    // Double ForEach
    public static void ForEachDouble<T>(this List<List<T>> list, Action<T> action)
    {
        foreach (var sublist in list)
            foreach (var item in sublist) action(item);
    }
    public static void ForEachDouble<T>(this List<T>[] list, Action<T> action)
    {
        foreach (var sublist in list)
            foreach (var item in sublist) action(item);
    }

    // Shuffle
    // private static Random rng = new Random();
    public static List<T> Shuffle<T>(this List<T> me)
    {
        Random rng = new();
        int n = me.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = me[k];
            me[k] = me[n];
            me[n] = value;
        }
        return me;
        // return me.OrderBy(a => rng.Next()).ToList();
    }

    public static T GetInactive<T>(this List<T> me) where T : UnityEngine.Component
    {
        var first = me.FirstOrDefault(e => e.gameObject.activeSelf == false);
        if (first == null)
        {
            UnityEngine.Debug.LogError($"No Inactive {typeof(T).Name} found");
            var prefab = me[0].gameObject;
            var clone = UnityEngine.Object.Instantiate(prefab, prefab.transform.parent).GetComponent<T>();
            me.Add(clone);
            return clone;
        };
        return first;
    }
    public static T GetInactive<T>(this T[] me) where T : UnityEngine.Component
    {
        var first = me.FirstOrDefault(e => e.gameObject.activeSelf == false);
        if (first == null) return null;
        first.gameObject.SetActive(true);
        return first;
    }


    public static bool AddIfNotExists<T>(this List<T> list, T value)
    {
        if (list.Contains(value) == false)
        {
            list.Add(value);
            return true;
        }
        return false;
    }

    #region Dictionary
    public static TV GetValueOrNew<TK, TV>(this IDictionary<TK, TV> d, TK k) where TV : new()
    {
        if (d.ContainsKey(k) == false) d.Add(k, new TV());
        return d[k];
    }
    public static int GetValueOrNew<TKey>(this IDictionary<TKey, int> d, TKey k, int v = 0)
    {
        if (d.ContainsKey(k) == false) d.Add(k, v);
        return d[k];
    }
    public static void Cumulate<TKey>(this IDictionary<TKey, int> d, TKey k, int add)
    {
        if (d.ContainsKey(k) == false) d.Add(k, add);
        else d[k] += add;
    }

    public static void RemoveAll<K, V>(this IDictionary<K, V> dict, Func<V, bool> match)
    {
        foreach (var key in dict.Keys.ToArray().Where(key => match(dict[key])))
            dict.Remove(key);
    }
    public static void RemoveAll<K, V>(this IDictionary<K, V> dict, Func<K, V, bool> match)
    {
        foreach (var key in dict.Keys.ToArray().Where(key => match(key, dict[key])))
            dict.Remove(key);
    }

    public static T[] GetRemovedAll<T>(this T[] array, Func<T, bool> match)
    {
        return array.Where(e => match(e)).ToArray();
    }
    #endregion
}
