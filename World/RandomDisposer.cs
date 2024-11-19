using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RandomDisposer : MonoBehaviour
{
    [Immutable] public int cnt_notDisposed = 0;
    [Header("Settings")]
    public int max_try = 10;
    public string layerMask = "NavGround";

    [Header("in the Circle?")]
    public float radius;

    [Header("in the Rect?")]
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
        cnt_notDisposed = 0;
        foreach (Transform c in transform)
        {
            var isOn = chance.Percent();
            c.SetActive(isOn);
            if (isOn == false) continue;

            // Try Disposing
            DisposeRandomly(c);
            var col = c.GetChild(0).GetComponent<Collider2D>();
            // Like DoWhile
            var isOverlapped = false;
            for (int i = 0; i < max_try; i++)
            {
                isOverlapped = IsOverlapped(col);
                if (isOverlapped == false) break;
                DisposeRandomly(c);
            }
            // Still Haven't Disposed?
            if (isOverlapped)
            {
                c.SetActive(false);
                cnt_notDisposed++;
            }
        }
    }

    bool IsOverlapped(Collider2D c)
    {
        var colls = new Collider2D[] { };
        var filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask(layerMask));
        c.Overlap(filter, colls);
        if (colls.Length > 0) Debug.Log($"<color=cyan>{colls.Length}</color>");
        return colls.Exists(e => e.CompareTag(c.tag));
    }

    void DisposeRandomly(Transform t)
    {
        if (radius != 0)
            t.localPosition = VectorEx.Random2(radius);
        else if (width != 0 || height != 0)
            t.localPosition = new Vector2(RandomEx.R(width, -width), RandomEx.R(height, -height));
    }
}
