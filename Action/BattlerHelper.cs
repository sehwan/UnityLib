using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class BattlerHelper
{ 
    public static GameObject FindClosest(Transform me, GameObject[] targets)
    {
        GameObject r = null;

        float min = Mathf.Infinity;
        float dist = 0;
        foreach (GameObject c in targets)
        {
            dist = Vector2.Distance(me.localPosition, c.transform.localPosition);
            if (dist < min)
            {
                min = dist;
                r = c;
            }
        }
        return r;
    }

    public static bool IsCloseEnough(Transform me, Vector3 target, float range)
    {
        return Vector2.Distance(me.transform.localPosition, target) < range;
    }




    public static float max_x, min_x, max_y, min_y;
    public static void Boundarize(Transform me)
    {
        me.localPosition = new Vector2(
            Mathf.Clamp(me.position.x, min_x, max_x),
            Mathf.Clamp(me.position.y, min_y, max_y));
    }

    public static void MakeAttack(Transform me)
    {

    }
}


public class BattleHelperMono : MonoSingleton<BattleHelperMono>
{
    public static void Wander(Transform me, float speed)
    {
        float time = RandomEx.R(5);
        float progress = 0;
        Vector2 dir = UnityEngine.Random.insideUnitCircle.normalized;
        progress += Time.deltaTime;
        me.transform.Translate(dir.normalized * Time.deltaTime * speed);
    }
}