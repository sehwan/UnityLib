using UnityEngine;
using System.Collections;
using System.Linq;

public class FPSIndicator : MonoBehaviour
{
    GUIStyle style = new();
    float[] frames = new float[10];
    float fps;

    private IEnumerator Start()
    {
        GUI.depth = 2;
        style.fontSize = Screen.width / 30;
        var i = 0;
        while (true)
        {
            fps = 1f / Time.unscaledDeltaTime;
            frames[i++] = fps;
            if (i >= frames.Length) i = 0;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(15, 15, 130, 40), $"{Mathf.Round(frames.Average())} ({Mathf.Round(fps)})");
    }
}