using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleAnimationLoop : MonoBehaviour
{
    public SpriteRenderer ren;
    public Sprite[] sprites;
    public float delay = 0.25f;

    void OnEnable()
    {
        StartCoroutine(Co_Animate());
    }

    public void SetSprites(Sprite[] sprites, float delay)
    {
        this.sprites = sprites;
        this.delay = delay;
    }
    public IEnumerator Co_Animate()
    {
        // var w = CoroutineEx.GetWait(delay);
        var w = new WaitForSeconds(delay);
        while (true)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                ren.sprite = sprites[i];
                yield return w;
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SimpleAnimationLoop))]
public class SimpleAnimationLoopEditor : Editor
{
    void OnEnable()
    {
        var s = (SimpleAnimationLoop)target;
        s.ren = s.GetComponent<SpriteRenderer>();
    }
    void OnDisable()
    {
        EditorApplication.update -= Update;
    }

    int iter;
    System.Diagnostics.Stopwatch stopwatch = new();
    void Update()
    {
        var s = (SimpleAnimationLoop)target;
        var timer = stopwatch.ElapsedMilliseconds / 1000f;
        if (timer >= s.delay)
        {
            stopwatch.Restart();
            s.ren.sprite = s.sprites[iter++ % s.sprites.Length];
        }
    }

    bool isPlaying;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (isPlaying)
        {
            if (GUILayout.Button("Stop"))
            {
                isPlaying = false;
                stopwatch.Stop();
                EditorApplication.update -= Update;
            }
        }
        else
        {
            if (GUILayout.Button("Play"))
            {
                isPlaying = true;
                stopwatch.Restart();
                EditorApplication.update += Update;
            }
        }
    }
}
#endif