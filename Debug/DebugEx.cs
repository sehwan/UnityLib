
using UnityEngine;

public static class DebugEx
{
    public static void DrawCircle(Color col, Vector2 pos, float rad, int seg, float dur)
    {
#if UNITY_EDITOR
        float x;
        float y;
        float xx = 0;
        float yy = 0;
        float angle = 20f;


        for (int i = 0; i < (seg + 1); i++)
        {
            x = pos.x + Mathf.Sin(Mathf.Deg2Rad * angle) * rad;
            y = pos.y + Mathf.Cos(Mathf.Deg2Rad * angle) * rad;

            if (i > 0)
                Debug.DrawLine(new Vector2(x, y), new Vector2(xx, yy), col, dur);
            xx = x;
            yy = y;
            angle += 360f / seg;
        }
#endif
    }

    public static void DrawBox(Color color, Vector2 midPoint, float w, float h, float angle, float dur)
    {
#if UNITY_EDITOR
        var size = new Vector2(w, h);
        var p11 = midPoint + RotateVector(new Vector2(-size.x, size.y) / 2, angle);
        var p1 = midPoint + RotateVector(new Vector2(size.x, size.y) / 2, angle);
        var p5 = midPoint + RotateVector(new Vector2(size.x, -size.y) / 2, angle);
        var p7 = midPoint + RotateVector(new Vector2(-size.x, -size.y) / 2, angle);
        Debug.DrawLine(p11, p1, color, dur);
        Debug.DrawLine(p1, p5, color, dur);
        Debug.DrawLine(p5, p7, color, dur);
        Debug.DrawLine(p7, p11, color, dur);
#endif
    }

    static Vector2 RotateVector(Vector2 v, float degrees)
    {
        var radians = degrees * Mathf.Deg2Rad;
        var sin = Mathf.Sin(radians);
        var cos = Mathf.Cos(radians);
        var tx = v.x;
        var ty = v.y;
        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }
}