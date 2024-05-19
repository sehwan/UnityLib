using System;
using System.Globalization;
using UnityEngine;

public static class NumberEx
{
    public static int IncreasedPercent(this int me, int percent)
    {
        return Mathf.CeilToInt(me * (1 + percent * 0.01f));
    }
    public static int IncreasedPercent(this int me, float percent)
    {
        return Mathf.CeilToInt(me * (1 + percent * 0.01f));
    }
    public static float IncreasedPercent(this float me, float percent)
    {
        return me * (1 + percent * 0.01f);
    }
    public static float Geometric(this float me, float percent, int count)
    {
        return me * Mathf.Pow(1 + percent * 0.01f, count);
    }
    public static BigNumber Geometric(this BigNumber me, float percent, int count)
    {
        return me * Mathf.Pow(1 + percent * 0.01f, count);
    }

    public static float NegateIf(this float me, bool condition)
    {
        return condition ? -me : me;
    }

    public static int Pow(this int me, int pow)
    {
        if (pow == 0) return 1;
        if (pow == 1) return me;

        int r = 1;
        while (pow != 0)
        {
            if ((pow & 1) == 1)
                r *= me;
            me *= me;
            pow >>= 1;
        }
        return r;
    }
    public static float Pow(this int me, float pow)
    {
        return Mathf.Pow(me, pow);
    }
    public static float Pow(this float me, float pow)
    {
        return Mathf.Pow(me, pow);
    }
    public static int Ceil(this float me)
    {
        return Mathf.CeilToInt(me);
    }

    public static float DivideSafe(this int numerator, int denominator)
    {
        return (numerator == 0 || denominator == 0) ? 0 : (float)numerator / (float)denominator;
    }
    public static float DivideSafe(this float Numerator, float Denominator)
    {
        return (Numerator == 0 || Denominator == 0) ? 0 : Numerator / Denominator;
    }
    public static float DivideSafe(this int Numerator, float Denominator)
    {
        return (Numerator == 0 || Denominator == 0) ? 0 : Numerator / Denominator;
    }
    public static float DivideSafe(this float Numerator, int Denominator)
    {
        return (Numerator == 0 || Denominator == 0) ? 0 : Numerator / Denominator;
    }

    const string th = "th";
    public static string ToOrdinal(this int number, string king = null)
    {
        // King?
        if (king.IsFilled() && number == 1) return king.L();

        var n = number.ToString();

        // Negative and zero have no ordinal representation
        if (number < 1) return "-";

        number %= 100;
        if (number >= 11 && number <= 13) return n + th;

        switch (number % 10)
        {
            case 1: return n + "st";
            case 2: return n + "nd";
            case 3: return n + "rd";
            default: return n + th;
        }
    }

    public static float Truncate(this float n, int place)
    {
        var x = 10 * place;
        return (float)Math.Truncate(x * n) / x;
    }
    public static float Round(this float n, int place)
    {
        return (float)Math.Round(n, place);
    }

    public static string ToKMB(this decimal num)
    {
        if (num > 999999999 || num < -999999999)
        {
            return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);
        }
        else
        if (num > 999999 || num < -999999)
        {
            return num.ToString("0,,.##M", CultureInfo.InvariantCulture);
        }
        else
        if (num > 999 || num < -999)
        {
            return num.ToString("0,.#K", CultureInfo.InvariantCulture);
        }
        else
        {
            return num.ToString(CultureInfo.InvariantCulture);
        }
    }
    public static int Abs(this int n)
    {
        return (n < 0) ? -n : n;
    }
    public static float Abs(this float n)
    {
        return (n < 0) ? -n : n;
    }

    // Triangular Wave
    public static float TriangularWave(float time)
    {
        return Mathf.Abs(time % 2 - 1);
    }
    // Square Wave
    public static float SquareWave(float period, float oscillating)
    {
        return Mathf.Abs(Time.time % period - oscillating) < period / 2 ? oscillating : 0;
    }
    // Curvy Triangular Wave
    //     y = pow(abs((x++ % 6) - 3), 2.0);
    // Concave curves(i.e.sqrt(x) shape):
    // y = pow(abs((x++ % 6) - 3), 0.5);
    
    public static bool IsBetween(this int me, int min, int max)
    {
        return me >= min && me <= max;
    }
}