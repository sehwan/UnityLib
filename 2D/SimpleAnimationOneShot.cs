using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleAnimationOneShot : MonoBehaviour
{
    public SpriteRenderer ren;
    public Sprite[] sprites;
    public float frametime = 0.1f;
    public bool isPlaying;

    public async Awaitable Play(string path, int count, float delay)
    {
        sprites = new Sprite[count];
        for (int i = 0; i < count; i++)
            sprites[i] = Resources.Load<Sprite>($"{path}{i}");
        await Play(sprites, delay);
    }
    public async Awaitable Play(Sprite[] sprites, float frametime)
    {
        this.sprites = sprites;
        this.frametime = frametime;
        ren.sprite = sprites[0];
        gameObject.SetActive(true);
        await Play();
    }

    void OnEnable()
    {
        if (isPlaying) return;
        Play();
    }
    public async Awaitable Play()
    {
        isPlaying = true;
        for (int i = 0; i < sprites.Length; i++)
        {
            ren.sprite = sprites[i];
            var split = ren.sprite.name.Split("__");
            if (split.Length > 1)
            {
                var time = float.Parse(split[1]);
                await Awaitable.WaitForSecondsAsync(time * frametime);
            }
            else await Awaitable.WaitForSecondsAsync(frametime);
        }
        gameObject.SetActive(false);
        isPlaying = false;
    }

    public IEnumerator Co_Test()
    {
        var w = new WaitForSeconds(frametime);
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
        if (timer >= s.frametime)
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