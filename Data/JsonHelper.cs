using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JsonHelper
{
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }


    public static T[] ToArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    public static List<T> ToList<T>(string json)
    {
        T[] array = ToArray<T>(json);
        List<T> list = new();
        for (int i = 0; i < array.Length; i++)
        {
            list.Add(array[i]);
        }
        return list;
    }


    public static string ToJson<T>(T[] array, bool prettyPrint = false)
    {
        Wrapper<T> wrapper = new()
        {
            array = array
        };
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
}