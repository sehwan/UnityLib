using UnityEngine;
using UnityEngine.UI;

public static class ColorEx
{
    public static Color none = new(0, 0, 0, 0);
    public static Color dim = new(0, 0, 0, .5f);

    public static Color lightGray = new(.8f, .8f, .8f);
    public static Color dark = new(.2f, .2f, .2f);
    public static Color darker = new(.135f, .135f, .135f);
    public static Color darkA = new(0.15f, 0.15f, 0.15f, 0.8f);

    // Red
    public static Color crimson = new(0.5f, 0f, 0f);
    public static Color darkRed = new(0.2f, 0f, 0f);
    public static Color redBrown = new(.4f, .1f, 0);

    // Yellow
    public static Color orange = new(1, .5f, 0);
    public static Color darkOrange = new(1, .2f, 0);
    public static Color gold = new(1, .74f, 0.26f);
    public static Color desert = new(0.4f, 0.2f, 0.0f);
    public static Color brown = new(0.6f, 0.3f, 0.2f);

    // Green
    public static Color green = new(.0f, .5f, .0f);
    public static Color darkGreen = new(.0f, .2f, .0f);
    public static Color lime = new(.7f, 1f, .7f);
    public static Color mint = new(.4f, 1f, .4f);
    public static Color emerald = new(.4f, .9f, .6f);

    // Blue
    public static Color darkBlue = new(0f, 0f, 0.2f);
    public static Color myBlue = new(.15f, .23f, .7f);
    public static Color myBlueDark = new(.05f, .08f, .23f);
    public static Color egiptianBlue = new(.06f, .2f, .65f);
    public static Color azure = new(.0f, .5f, 1f);
    public static Color ultraMarine = new(.25f, 0f, 1f);
    public static Color blue = new(0f, 0.3f, 7f);
    public static Color cyanDirty = new(0, 0.7f, 0.7f);

    public static Color purple = new(0.8f, 0, 0.6f);
    public static Color pink = new(1, 0.5f, 0.5f);
    
    // Violet
    public static Color violet = new(0.533f, 0.271f, 1f);
    public static Color violetDark = new(0.36f, 0f, 1f);

    // Damage
    public static Color ad = new(1f, .4f, .6f);
    public static Color ap = new(.6f, .5f, 1f);
    public static Color fire = new(1, .3f, .3f);
    public static Color ice = new Color(.1f, .85f, 1f);
    public static Color lightning = new Color(1, 1, 0);
    public static Color poison = Color.green;
    public static Color spirit = new Color(.2f, 0, .2f);

    //Grade
    public static Color[] GRADES ={
        new(.0f, .0f, .0f),
        new(.0f, .0f, .0f),
        // new(.15f, .15f, .0f),
        new(.0f, .15f, .0f),
        new(.0f, .05f, .18f),
        new(.15f, .0f, .15f),
    };
    public static Color[] GRADES_Colored ={
        Color.gray,
        lightGray,
        emerald,
        Color.cyan,
        Color.magenta,
        gold
    };

    public static Color RandomColor(this string s, float alpha = 1)
    {
        int seed = 0;
        foreach (char c in s)
        {
            seed += c;
        }
        Random.InitState(seed);
        return new Color(Random.value, Random.value, Random.value, alpha);
    }

    public static Color RandomColorWithMax(float max = 1)
    {
        return new Color(RandomForColor(max, 0), RandomForColor(max, 0), RandomForColor(max, 0));
    }
    public static Color RandomColorWithMin(float min = 0)
    {
        return new Color(RandomForColor(1, min), RandomForColor(1, min), RandomForColor(1, min));
    }
    public static Color RandomColorWithMinMax(float min = 0, float max = 1)
    {
        return new Color(RandomForColor(max, min), RandomForColor(max, min), RandomForColor(max, min));
    }
    static float RandomForColor(float max, float min)
    {
        var r = RandomEx.R(max, min);
        if (r > 1) return 1;
        if (r < 0) return 0;
        return r;
    }

    public static Color Add(this Color me, float p)
    {
        return me + new Color(p, p, p);
    }

    public static Color SetAlpha(this Color me, float a)
    {
        me.a = a;
        return me;
    }

    public static void SetAlpha(this SpriteRenderer me, float a)
    {
        var color = me.color;
        color.a = a;
        me.color = color;
    }
    public static void SetAlpha(this Image me, float a)
    {
        var color = me.color;
        color.a = a;
        me.color = color;
    }

    public static Color GreenToYellowToRed(this float me)
    {
        if (me > 0.5f)
            return Color.Lerp(Color.yellow, Color.green, me * 2f - 1f);
        else
            return Color.Lerp(Color.red, Color.yellow, me * 2f);
    }
}
