using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomTrees : MonoBehaviour
{
    [Header("Settings")]
    public int COLONY_CNT_MAX = 13;
    public int COLONY_CNT_MIN = 5;
    public float ROOMY = 1.75f;

    [Header("Boundary Settings")]
    public float width;
    public float height;


    void Start()
    {
        var rot = CameraWork.i.transform.rotation;
        // var rot = Quaternion.Euler(-90, 0, 0);
        foreach (Transform c in transform) c.rotation = rot;
    }

    public void Randomize(int chance)
    {
        gameObject.SetActive(true);
        Vector2[] colonies = new Vector2[RandomEx.R(COLONY_CNT_MAX, COLONY_CNT_MIN)];
        for (int i = 0; i < colonies.Length; i++)
        {
            colonies[i] = new Vector2(
                Random.Range(-width, width),
                Random.Range(-height, height));
        }
        foreach (Transform e in transform)
        {
            e.SetActive(false);
            if (chance.Percent() == false) continue;
            // Dispose
            e.SetActive(true);
            e.position = colonies[Random.Range(0, colonies.Length)];
            e.position += VectorEx.Random3(ROOMY).GetModifiedZ(0);
        }
    }
    public void Colorize(Color color)
    {
        foreach (Transform c in transform)
        {
            c.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
