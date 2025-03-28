using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class RandomEx
{
    public static int RandomWithSeedToday(int max)
    {
        Random rnd = new(DateTime.Now.DayOfYear);
        return rnd.Next(max);
    }
    public static int RandomWithSeed(int seed, int max)
    {
        Random rnd = new(seed);
        return rnd.Next(max);
    }
    public static bool Percent(this float numerator, float denominator = 100)
    {
        // Random rnd = new Random(Guid.NewGuid().GetHashCode());
        float r = UnityEngine.Random.Range(0, denominator);
        if (r <= numerator) return true;
        else return false;
    }
    public static bool Percent(this int numerator, int denominator = 100)
    {
        if (numerator < 0) return false;
        float r = UnityEngine.Random.Range(0, denominator);
        if (r < numerator) return true;
        else return false;
    }

    #region Random Number
    //bool
    public static bool Random(this bool me)
    {
        return UnityEngine.Random.value > 0.5f;
    }

    // Don't Add This Keyword To Prevent Confusion
    // -1.R() Must Be (-1).R()
    // So Just Use RandomEx.R(-1)
    public static int Range(this int me)
    {
        return UnityEngine.Random.Range(0, me);
    }
    public static int R(this int max, int min = 0)
    {
        if (min > max)
        {
            var tmp = min;
            min = max;
            max = tmp;
        }
        return UnityEngine.Random.Range(min, max);
    }
    public static int R(this long max, long min = 0)
    {
        if (min > max) (max, min) = (min, max);
        return UnityEngine.Random.Range((int)min, (int)max);
    }
    public static float R(this float max, float min = 0)
    {
        if (min > max)
        {
            var tmp = min;
            min = max;
            max = tmp;
        }
        return UnityEngine.Random.Range(min, max);
    }

    public static int Randomize(this int me, int percent)
    {
        float variance = me * percent / 100f;
        return (int)(me + R(variance, -variance));
    }
    public static float RandomizeByPercents(this float me, int percent)
    {
        float variance = me * percent / 100f;
        return me + R(variance, -variance);
    }
    #endregion


    #region Sample
    public static T SampleEnum<T>(int starting = 0, bool hasCount = false)
    {
        var values = Enum.GetValues(typeof(T));
        var max = values.Length;
        if (hasCount) max--;
        return (T)values.GetValue(UnityEngine.Random.Range(starting, max));
    }

    public static TValue Sample<TKey, TValue>(this IDictionary<TKey, TValue> me)
    {
        return me.ElementAt(UnityEngine.Random.Range(0, me.Count)).Value;
    }
    public static T Sample<T>(this List<T> me)
    {
        return me.ElementAt(UnityEngine.Random.Range(0, me.Count));
    }
    public static T Sample<T>(this T[] me)
    {
        return me[UnityEngine.Random.Range(0, me.Length)];
    }
    public static T Sample<T>(this ICollection<T> me)
    {
        return me.ElementAt(UnityEngine.Random.Range(0, me.Count));
    }
    public static T Sample<T>(this IEnumerable<T> me)
    {
        return me.ElementAt(UnityEngine.Random.Range(0, me.Count()));
    }
    public static T SampleOfToday<T>(this IEnumerable<T> me)
    {
        return me.ElementAt(RandomWithSeedToday(me.Count()));
    }

    public static List<T> SamplesUnDuplicated<T>(this List<T> me, int max)
    {
        max = Math.Min(max, me.Count);
        List<T> list = new();
        for (int i = 0; i < max; i++)
        {
            T t;
            int r = 0;
            do
            {
                r = R(me.Count);
                t = me[r];
            } while (list.Contains(t));
            list.Add(t);
        }
        return list;
    }

    public static int[] IntArray_UnDuplicated(int min, int max, int count = 0)
    {
        if (count == 0) count = max - 1;
        int[] arr = new int[count];
        for (int i = 0; i < count; i++)
        {
            int r = R(max, min);
            while (arr.Contains(r))
            {
                r = R(max, min);
            }
            arr[i] = r;
        }
        return arr;
    }

    public static T SampleByChance<T>(this List<T> list, Func<T, int> chance, int max = 100)
    {
        int r = R(max);
        int accumulated = 0;
        foreach (var item in list)
        {
            accumulated += chance(item);
            if (accumulated > r)
            {
                return item;
            }
        }
        return default(T);
    }


    // Pick the Heavy more
    public static T SampleWeighted<T>(this List<T> list, Func<T, int> weight)
    {
        var total = list.Sum(e => weight(e));
        Random rnd = new(Guid.NewGuid().GetHashCode());
        var r = rnd.Next(total);
        var weighted = 0;
        foreach (var item in list)
        {
            weighted += weight(item);
            if (r < weighted)
                return item;
        }
        throw new ArgumentException("Collection count and weights must be greater than 0");
    }

    public static T SampleLight<T>(this IEnumerable<T> dic, Func<T, float> weight)
    {
        float total = dic.Sum(e => weight(e));
        float totalR = dic.Sum(e => total / weight(e));
        // UnityEngine.Debug.Log($"<color=blue>{total} {totalR}</color>");
        var r = UnityEngine.Random.Range(0, totalR);
        float weighted = 0;
        foreach (var item in dic)
        {
            float e = weight(item);
            float w = total / e;
            weighted += w;
            if (r < weighted) return item;
        }
        throw new ArgumentException("Collection count and weights must be greater than 0");
    }
    public static T SampleLight<T>(this T[] list, Func<T, int> weight)
    {
        var total = list.Sum(e => weight(e));
        var totalR = list.Sum(e => total / weight(e));
        // UnityEngine.Debug.Log($"<color=magenta>{total}/{totalR}</color>");
        // UnityEngine.Debug.Log($"<color=magenta>{100 * total / totalR:f2}%-{100 * total / 5 / totalR:f2}%</color>");
        Random rnd = new(Guid.NewGuid().GetHashCode());
        var r = rnd.Next(totalR);
        float weighted = 0;
        foreach (var item in list)
        {
            float e = weight(item);
            float w = total / e;
            weighted += w;
            if (r < weighted) return item;
        }
        throw new ArgumentException("Collection count and weights must be greater than 0");
    }
    public static T SampleLight<T>(this List<T> list, Func<T, int> weight)
    {
        var total = list.Sum(e => weight(e));
        var totalR = list.Sum(e => total / weight(e));
        // UnityEngine.Debug.Log($"<color=blue>{total} {totalR}</color>");
        Random rnd = new(Guid.NewGuid().GetHashCode());
        var r = rnd.Next(totalR);
        float weighted = 0;
        foreach (var item in list)
        {
            float e = weight(item);
            float w = total / e;
            weighted += w;
            if (r < weighted) return item;
        }
        throw new ArgumentException("Collection count and weights must be greater than 0");
    }
    public static T SampleLight<T>(this List<T> list, Func<T, float> weight)
    {
        float total = list.Sum(e => weight(e));
        float totalR = list.Sum(e => total / weight(e));
        // UnityEngine.Debug.Log($"<color=blue>{total} {totalR}</color>");
        var r = UnityEngine.Random.Range(0, totalR);
        float weighted = 0;
        foreach (var item in list)
        {
            float e = weight(item);
            float w = total / e;
            weighted += w;
            if (r < weighted) return item;
        }
        throw new ArgumentException("Collection count and weights must be greater than 0");
    }
    public static T SampleLight<T>(this Dictionary<string, T> dic, Func<T, int> weight)
    {
        float total = dic.Sum(e => weight(e.Value));
        float totalR = dic.Sum(e => total / weight(e.Value));
        // UnityEngine.Debug.Log($"<color=blue>{total} {totalR}</color>");
        var r = UnityEngine.Random.Range(0, totalR);
        float weighted = 0;
        foreach (var item in dic)
        {
            float e = weight(item.Value);
            float w = total / e;
            weighted += w;
            if (r < weighted) return item.Value;
        }
        throw new ArgumentException("Collection count and weights must be greater than 0");
    }
    public static T SampleLight<T>(this Dictionary<string, T> dic, Func<T, float> weight)
    {
        float total = dic.Sum(e => weight(e.Value));
        float totalR = dic.Sum(e => total / weight(e.Value));
        // UnityEngine.Debug.Log($"<color=blue>{total} {totalR}</color>");
        var r = UnityEngine.Random.Range(0, totalR);
        float weighted = 0;
        foreach (var item in dic)
        {
            float e = weight(item.Value);
            float w = total / e;
            weighted += w;
            if (r < weighted) return item.Value;
        }
        throw new ArgumentException("Collection count and weights must be greater than 0");
    }
    #endregion


    // 최대공약수
    public static float GreatestCommonDenominator(float a, float b)
    {
        float tmp;
        if (a <= 0 || b <= 0) { return -1; }

        while (float.IsNaN(a) && float.IsNaN(b) && b != 0)
        {
            tmp = b;
            b = a % b;
            a = tmp;
        }
        return a;
    }
    //최소 공배수
    public static float LeastCommonMultiple(float a, float b)
    {
        if (a <= 0 || b <= 0) { return -1; }
        return a * b / GreatestCommonDenominator(a, b);
    }


    // Shuffle
    public static void Shuffle<T>(this IList<T> list, Random rnd = null)
    {
        rnd ??= new();
        int n = list.Count;
        while (n > 1)
        {
            int k = rnd.Next(n--);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
    public static T[] Shuffle<T>(this T[] array, Random rnd = null)
    {
        rnd ??= new();
        int n = array.Length;
        while (n > 1)
        {
            int k = rnd.Next(n--);
            (array[k], array[n]) = (array[n], array[k]);
        }
        return array;
    }


    // Pick Star Grade
    public static int Gatcha(this int[] array)
    {
        int order = 0;
        float r = R(array.Sum());
        int chance = 0;
        for (int i = 0; i < array.Length; i++)
        {
            chance += array[i];
            if (r < chance) return i;
        }
        return order;
    }

    public static int Gatcha(this float[] array)
    {
        int order = 0;
        float r = R(array.Sum());
        float chance = 0;
        for (int i = 0; i < array.Length; i++)
        {
            chance += array[i];
            if (r < chance) return i;
        }
        return order;
    }
}
