using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RandomSpriteSpreader : MonoBehaviour
{
    public Sprite[] samples;
    public float chance;
    public short defSortingOrder;

    [Header("Transform")]
    public float width;
    public float height;
    public float depthMin, depthMax;
    public float scaleMin = 1, scaleMax = 3;

    [Header("Color")]
    public float saturationMin = 0;
    public float saturationMax = 1;
    public float valueMin = 0, valueMax = 1;
    public float alphaMin = 0, alphaMax = 1;


    void Start()
    {
        var camRotation = CameraWork.i.tr.rotation;
        foreach (Transform c in transform) c.rotation = camRotation;
    }

    public void Spread()
    {
        transform.ForEach(e =>
        {
            if (chance.Percent())
            {
                var ren = e.GetComponent<SpriteRenderer>();
                e.localPosition = new Vector3(
                    RandomEx.R(width, -width),
                    RandomEx.R(height, -height),
                    RandomEx.R(depthMax, depthMin));
                e.transform.SetActive(true);
                ren.sprite = samples.Sample();
                return;
            }
            e.transform.SetActive(false);
        });
    }
    public void ShowAll()
    {
        foreach (Transform c in transform) c.SetActive(true);
    }
    public void HideAll()
    {
        foreach (Transform c in transform) c.SetActive(false);
    }
    public void ColorRandom()
    {
        foreach (Transform c in transform) c.GetComponent<SpriteRenderer>().color =
            Random.ColorHSV(0, 1, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);
    }
    public void ScaleRandom()
    {
        foreach (Transform c in transform) c.localScale = Vector3.one * Random.Range(scaleMin, scaleMax);
    }
    public void RotateRandom()
    {
        foreach (Transform c in transform) c.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
    public void SortingOrderRandom()
    {
        var order = defSortingOrder;
        foreach (Transform c in transform) c.GetComponent<SpriteRenderer>().sortingOrder = order++;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RandomSpriteSpreader))]
public class RandomSpriteSpreaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var s = target as RandomSpriteSpreader;
        if (GUILayout.Button("Spread")) s.Spread();
        if (GUILayout.Button("Random Color")) s.ColorRandom();
        if (GUILayout.Button("Random Scale")) s.ScaleRandom();
        if (GUILayout.Button("Random Rotate")) s.RotateRandom();
        if (GUILayout.Button("Random Sorting Order")) s.SortingOrderRandom();
    }
}
#endif