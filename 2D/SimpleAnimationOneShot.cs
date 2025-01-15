using System.Collections;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleAnimationOneShot : MonoBehaviour
{
    public SpriteRenderer ren;
    public Sprite[] sprites;
    public float delay = 0.25f;


    void OnEnable()
    {
        PlayOneShot();
    }
    
    public async Awaitable PlayOneShot()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < sprites.Length; i++)
        {
            ren.sprite = sprites[i];
            await Awaitable.WaitForSecondsAsync(delay);
        }
        gameObject.SetActive(false);
    }

    public IEnumerator Co_Test()
    {
        var w = new WaitForSeconds(delay);
        for (int i = 0; i < sprites.Length; i++)
        {
            ren.sprite = sprites[i];
            yield return w;
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(SimpleAnimationOneShot))]
public class SimpleAnimationOneShotEditor : Editor
{
    void OnEnable()
    {
        var s = (SimpleAnimationOneShot)target;
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
        var s = (SimpleAnimationOneShot)target;
        var timer = stopwatch.ElapsedMilliseconds / 1000f;
        if (timer >= s.delay)
        {
            stopwatch.Restart();
            s.ren.sprite = s.sprites[iter++];
            if (iter >= s.sprites.Length)
            {
                EditorApplication.update -= Update;
                iter = 0;
                isPlaying = false;
            }
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