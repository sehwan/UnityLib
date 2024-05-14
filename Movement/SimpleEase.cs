using UnityEngine;

public static class SimpleEase
{
    //Linear. Pretty much the same as Mathf.Lerp
    public static float Linear(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return ApplyEase(start, end, t);
    }

    //Sinusodial ease functions
    public static float SinIn(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return ApplyEase(start, end, 1f - Mathf.Cos(t * (Mathf.PI / 2f)));
    }

    public static float SinOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return ApplyEase(start, end, Mathf.Sin(t * (Mathf.PI / 2f)));
    }

    public static float SinInOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return ApplyEase(start, end, -0.5f * (Mathf.Cos(Mathf.PI * t) - 1));
    }

    //Quadratic ease functions
    public static float QuadIn(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return ApplyEase(start, end, t * t);
    }

    public static float QuadOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return ApplyEase(start, end, -1f * t * (t - 2));
    }

    public static float QuadInOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        t *= 2f;

        float calculatedT;

        if (t < 1f)
        {
            calculatedT = 0.5f * t * t;
        }
        else
        {
            t--;
            calculatedT = -0.5f * (t * (t - 2) - 1);
        }

        return ApplyEase(start, end, calculatedT);
    }

    //Exponential ease functions
    public static float ExpoIn(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return ApplyEase(start, end, Mathf.Pow(2, 10 * (t - 1)));
    }

    public static float ExpoOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return ApplyEase(start, end, -Mathf.Pow(2, -10 * t) + 1);
    }

    public static float ExpoInOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        t *= 2f;

        float calculatedT;

        if (t < 1)
        {
            calculatedT = 0.5f * Mathf.Pow(2, 10 * (t - 1));
        }
        else
        {
            t--;
            calculatedT = 0.5f * (-Mathf.Pow(2, -10 * t) + 2);
        }

        return ApplyEase(start, end, calculatedT);
    }

    //Elastic ease functions (over shoots and go back)
    public static float ElasticIn(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        const float p = 0.3f;
        return ApplyEase(start, end, -(1 * Mathf.Pow(2, 10 * (t -= 1f)) * Mathf.Sin((t - p / 4f) * (2 * Mathf.PI) / p)));
    }

    public static float ElasticOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        const float p = 0.3f;
        return ApplyEase(start, end, Mathf.Pow(2, -10 * t) * Mathf.Sin((t - p / 4f) * (2 * Mathf.PI) / p) + 1);
    }

    public static float ElasticOutIn(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);

        t *= 2f;
        const float p = 1 * (0.3f * 1.5f);
        if (t < 1f)
        {
            return ApplyEase(start, end, -0.5f * (Mathf.Pow(2, 10 * (t -= 1f)) * Mathf.Sin((t * 1 - p / 4f) * (2 * Mathf.PI) / p)));
        }
        else
        {
            return ApplyEase(start, end, Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * 1 - p / 4f) * (2 * Mathf.PI) / p) * 0.5f + 1);
        }
    }

    //Bounce ease functions (not the most elegant implementation, but I couldn't find any better)
    public static float BounceOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);

        float calculatedT;

        if (t < (1 / 2.75f))
        {
            calculatedT = 7.5625f * t * t;
        }
        else if (t < (2 / 2.75f))
        {
            calculatedT = 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
        }
        else if (t < (2.5f / 2.75f))
        {
            calculatedT = 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
        }
        else
        {
            calculatedT = 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
        }

        return ApplyEase(start, end, calculatedT);
    }

    public static float BounceIn(float start, float end, float t)
    {
        return BounceOut(end, start, 1f - t);
    }

    public static float BounceInOut(float start, float end, float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        t *= 2f;

        if (t < 1f)
        {
            return BounceIn(start, Linear(start, end, 0.5f), t);
        }

        t--;
        return BounceOut(Linear(start, end, 0.5f), end, t);
    }

    private static float ApplyEase(float start, float end, float t)
    {
        return start + (end - start) * t;
    }
}