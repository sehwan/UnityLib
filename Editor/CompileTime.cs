using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
class CompileTime : EditorWindow
{
    static bool isTrackingTime;
    static double startTime;

    static CompileTime()
    {
        EditorApplication.update += Update;
        startTime = PlayerPrefs.GetFloat("CompileStartTime", 0);
        if (startTime > 0)
        {
            isTrackingTime = true;
        }
    }


    static void Update()
    {
        if (EditorApplication.isCompiling && !isTrackingTime)
        {
            startTime = EditorApplication.timeSinceStartup;
            PlayerPrefs.SetFloat("CompileStartTime", (float)startTime);
            isTrackingTime = true;
        }
        else if (!EditorApplication.isCompiling && isTrackingTime)
        {
            var finishTime = EditorApplication.timeSinceStartup;
            isTrackingTime = false;
            var compileTime = finishTime - startTime;
            PlayerPrefs.DeleteKey("CompileStartTime");
            if (compileTime >= 60)
            {
                int minutes = (int)(compileTime / 60);
                double seconds = compileTime % 60;
                Debug.Log($"Compiled in <color=cyan>{minutes}:{seconds:00}</color>");
            }
            else
            {
                Debug.Log($"Compiled in <color=cyan>{compileTime:0.00}s</color>");
            }
        }
    }
}