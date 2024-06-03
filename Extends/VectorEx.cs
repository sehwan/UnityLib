using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorEx
{
    public static Vector2 Random2(float radiusMax = 1)
    {
        return Random.insideUnitCircle * radiusMax;
    }
    public static Vector3 Random3(float radiusMax = 1)
    {
        return Random.insideUnitSphere * radiusMax;
    }
    public static Vector2 RandomPointOnCircle(float radius)
    {
        var angle = RandomEx.R(360);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }

    public static Vector2 RandomPos(this Collider2D me)
    {
        var bounds = me.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
    public static Vector2 RandomPos(this CircleCollider2D me)
    {
        float radius = me.radius;
        float angle = Random.Range(0, 2 * Mathf.PI);
        float distance = Random.Range(0, radius);
        float x = me.transform.position.x + distance * Mathf.Cos(angle);
        float y = me.transform.position.y + distance * Mathf.Sin(angle);
        return new Vector2(x, y);
    }
    // Modify
    public static Vector3 X0(this Vector3 v)
    {
        v.x = 0;
        return v;
    }
    public static Vector3 Y0(this Vector3 v)
    {
        v.y = 0;
        return v;
    }
    public static Vector3 Z0(this Vector3 v)
    {
        v.z = 0;
        return v;
    }

    public static Vector3 GetModifiedX(this Vector3 me, int v)
    {
        return new Vector3(v, me.y, me.z);
    }
    public static Vector3 GetModifiedY(this Vector3 me, int v)
    {
        return new Vector3(me.x, v, me.z);
    }
    public static Vector3 GetModifiedZ(this Vector3 me, int v)
    {
        return new Vector3(me.x, me.y, v);
    }

    public static Vector3 GetModifiedX(this Vector3 me, float v)
    {
        return new Vector3(v, me.y, me.z);
    }
    public static Vector3 GetModifiedY(this Vector3 me, float v)
    {
        return new Vector3(me.x, v, me.z);
    }
    public static Vector3 GetModifiedZ(this Vector3 me, float v)
    {
        return new Vector3(me.x, me.y, v);
    }

    // Add
    public static Vector2 Add(Vector3 a, Vector3 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    public static Vector3 AddX(this Vector3 me, float v)
    {
        return new Vector3(me.x + v, me.y, me.z);
    }
    public static Vector3 AddY(this Vector3 me, float v)
    {
        return new Vector3(me.x, me.y + v, me.z);
    }
    public static Vector3 AddZ(this Vector3 me, float v)
    {
        return new Vector3(me.x, me.y, me.z + v);
    }

    public static Vector2 AddX(this Vector2 me, float v)
    {
        return new Vector3(me.x + v, me.y);
    }
    public static Vector2 AddY(this Vector2 me, float v)
    {
        return new Vector2(me.x, me.y + v);
    }

    public static Vector3 Add(this Vector3 me, Vector2 v)
    {
        return new Vector3(me.x + v.x, me.y + v.y, me.z);
    }
    public static Vector2 Add(this Vector2 me, Vector3 v)
    {
        return new Vector2(me.x + v.x, me.y + v.y);
    }

    // Transform
    public static Vector2 V2(this Vector3 me) => (Vector2)me;
    public static Vector3 V3(this Vector2 me) => new(me.x, me.y);

    public static Vector3 WorldToUISpace(Canvas canvas, Vector3 worldPos, Camera cam)
    {
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out movePos);
        return canvas.transform.TransformPoint(movePos);
    }
    public static Vector3 WorldToUISpace(this Vector3 worldPos, Canvas canvas, Camera cam)
    {
        return WorldToUISpace(canvas, worldPos, cam);
    }

    // Battle
    public enum Direction4 { Up, Down, Left, Right }
    public static Direction4 WhichDir4(this Vector2 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle > -45 && angle <= 45) return Direction4.Right;
        else if (angle > 45 && angle <= 135) return Direction4.Up;
        else if (angle > -135 && angle <= -45) return Direction4.Down;
        else return Direction4.Left;
    }
    public static Direction4 WhichDir4(this Vector3 dir)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle > -45 && angle <= 45) return Direction4.Right;
        else if (angle > 45 && angle <= 135) return Direction4.Up;
        else if (angle > -135 && angle <= -45) return Direction4.Down;
        else return Direction4.Left;
    }
    public static Vector2 HumanizedTrajectoryDest(this Vector3 from, Vector2 to, float humanizing)
    {
        if (humanizing == 0) return to;
        var totalDist = Vector2.Distance(from, to);
        var offset = humanizing * totalDist * Random.insideUnitCircle;
        to += offset;
        return to;
    }
}
