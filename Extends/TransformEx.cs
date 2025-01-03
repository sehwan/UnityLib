using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class TransformEx
{
    public static void SetActive(this Transform me, bool isActive)
    {
        me.gameObject.SetActive(isActive);
    }
    public static void SetActive(this ParticleSystem me, bool isActive)
    {
        me.gameObject.SetActive(isActive);
    }

    public static void SetParent(this GameObject me, Transform parent)
    {
        me.transform.SetParent(parent);
    }
    public static void ForEach(this Transform me, Action<Transform> cb)
    {
        foreach (Transform c in me)
        {
            cb(c);
        }
    }

    public static void Pool(this Transform me, int poolSize)
    {
        var prefab = me.GetChild(0).gameObject;
        int remains = me.childCount;
        for (int i = 0; i < poolSize - remains; i++)
        {
            GameObject.Instantiate(prefab, me);
        }
        foreach (Transform item in me) item.gameObject.SetActive(false);
    }
    public static T[] Pool<T>(this Transform me, int poolSize)
    {
        var prefab = me.GetChild(0).gameObject;
        int remains = me.childCount;
        for (int i = 0; i < poolSize - remains; i++)
        {
            GameObject.Instantiate(prefab, me);
        }
        T[] arr = new T[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            arr[i] = me.GetChild(i).GetComponent<T>();
        }
        foreach (Transform item in me) item.gameObject.SetActive(false);
        return arr;
    }
    public static void Pool(this Transform me, GameObject prefab, int poolSize)
    {
        int remains = me.childCount;
        for (int i = 0; i < poolSize - remains; i++)
            GameObject.Instantiate(prefab, me);
        foreach (Transform item in me) item.gameObject.SetActive(false);
    }
    public static T[] Pool<T>(this Transform me, GameObject prefab, int poolSize)
    {
        int remains = me.childCount;
        for (int i = 0; i < poolSize - remains; i++)
        {
            GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, me);
        }
        foreach (Transform item in me) item.gameObject.SetActive(false);
        T[] arr = new T[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            arr[i] = me.GetChild(i).GetComponent<T>();
        }
        return arr;
    }
    public static T[] ChildrenToArray<T>(this Transform me)
    {
        var cnt = me.childCount;
        T[] arr = new T[cnt];
        for (int i = 0; i < cnt; i++) arr[i] = me.GetChild(i).GetComponent<T>();
        return arr;
    }

    public static Transform Find(this Transform me, Func<Transform, bool> cb)
    {
        foreach (Transform c in me) if (cb(c)) return c;
        return null;
    }

    public static void LookCamera(this Transform me)
    {
        me.rotation = CameraWork.i.transform.rotation;
    }
    public static void Look(this Transform me, Vector2 target)
    {
        me.rotation = Quaternion.LookRotation(Vector3.forward, target);
    }
    public static void Look2D(this Transform me, Vector3 target)
    {
        var dir = me.position - target;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        me.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // 
    public static void MultiLocalScale(this Transform me, float v)
    {
        me.transform.localScale *= v;
    }
    public static void LocalScale(this Transform me, float v)
    {
        me.transform.localScale = new Vector3(v, v, v);
    }
    public static void LocalScaleX(this Transform me, float v)
    {
        me.transform.localScale = new Vector3(v, me.transform.localScale.y, me.transform.localScale.z);
    }
    public static void FlipX(this Transform me, bool isFlipX)
    {
        var abs_x = Mathf.Abs(me.transform.localScale.x);
        if (isFlipX) me.transform.LocalScaleX(-abs_x);
        else me.transform.LocalScaleX(abs_x);
    }
    public static void LocalScaleY(this Transform me, float v)
    {
        me.transform.localScale = new Vector3(me.localScale.x, v, me.localScale.z);
    }
    public static void LocalScaleZ(this Transform me, float v)
    {
        me.transform.localScale = new Vector3(me.localScale.x, me.localScale.y, v);
    }
    public static void LocalScale(this Transform me, float x, float y)
    {
        me.transform.localScale = new Vector2(x, y);
    }
    public static void LocalScale(this Transform me, float x, float y, float z)
    {
        me.transform.localScale = new Vector3(x, y, z);
    }

    // Position
    public static void LocalPositionX(this Transform me, float v)
    {
        me.transform.localPosition = new Vector3(v, me.localPosition.y, me.localPosition.z);
    }
    public static void LocalPositionY(this Transform me, float v)
    {
        me.transform.localPosition = new Vector3(me.localPosition.x, v, me.localPosition.z);
    }
    public static void LocalPositionZ(this Transform me, float v)
    {
        me.transform.localPosition = new Vector3(me.localPosition.x, me.localPosition.y, v);
    }
    public static void LocalPosition(this Transform me, float x, float y, float z)
    {
        me.transform.localPosition = new Vector3(x, y, z);
    }
    public static void LocalPosition(this Transform me, float x, float y)
    {
        me.transform.localPosition = new Vector2(x, y);
    }

    public static void PositionX(this Transform me, float v)
    {
        me.transform.position = new Vector3(v, me.position.y, me.position.z);
    }
    public static void PositionY(this Transform me, float v)
    {
        me.transform.position = new Vector3(me.position.x, v, me.position.z);
    }
    public static void PositionZ(this Transform me, float v)
    {
        me.transform.position = new Vector3(me.position.x, me.position.y, v);
    }
    public static void Position(this Transform me, float x, float y)
    {
        me.position = new Vector2(x, y);
    }
    public static void Position(this Transform me, float x, float y, float z)
    {
        me.position = new Vector3(x, y, z);
    }

    public static int SignX(this Transform me)
    {
        return me.position.x > 0 ? 1 : -1;
    }

    // Children
    public static void ShuffleChildren(this Transform me)
    {
        List<int> list = new();
        var cnt = me.childCount;
        for (int i = 0; i < cnt; i++)
        {
            list.Add(i);
        }
        list.Shuffle();

        for (int i = 0; i < cnt; i++)
        {
            me.GetChild(i).SetSiblingIndex(list[i]);
        }
    }
    public static void DestroyAllChildren(this Transform me)
    {
        foreach (Transform c in me) GameObject.Destroy(c.gameObject);
    }
    public static void SetActiveAllChildren(this Transform me, bool isActive)
    {
        foreach (Transform c in me) c.gameObject.SetActive(isActive);
    }
    public static T[] GetComponentsInChildrenWOSelf<T>(this Transform me, bool includeInactive = false)
    {
        var components = me.GetComponentsInChildren<T>(includeInactive);
        var selfComponents = me.GetComponents<T>();
        return components.Except(selfComponents).ToArray();
    }
    public static List<Transform> FindChildrenRecursive(this Transform me, string name, List<Transform> founds = null)
    {
        List<Transform> rt;
        if (founds != null) rt = founds;
        else rt = new List<Transform>();

        foreach (Transform c in me)
        {
            if (c.name == name) rt.Add(c);
            FindChildrenRecursive(c, name, rt);
        }
        return rt;
    }
    public static GameObject FindChildRecursively(this GameObject parent, string path)
    {
        string[] pathSegments = path.Split('/');

        if (pathSegments.Length == 0)
            return null;

        Transform childTransform = parent.transform.Find(pathSegments[0]);

        if (childTransform == null)
            return null;

        if (pathSegments.Length == 1)
            return childTransform.gameObject;

        string remainingPath = string.Join("/", pathSegments, 1, pathSegments.Length - 1);

        return FindChildRecursively(childTransform.gameObject, remainingPath);
    }
    public static Transform FirstInactiveChild(this Transform me)
    {
        foreach (Transform c in me)
            if (c.gameObject.activeSelf == false) return c;
        return null;
    }

    public static GameObject FirstInactive(this GameObject[] gos)
    {
        foreach (var go in gos)
            if (go.activeSelf == false) return go;
        return null;
    }

    public static T FirstInactive<T>(this T[] items) where T : Component
    {
        foreach (var item in items)
        {
            if (!item.gameObject.activeSelf)
                return item;
        }
        return null;
    }
}
