
// 씬관리자 조상.
// 필요할까?

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(InputKeyActivity))]
[RequireComponent(typeof(AudioSource))]
public class SceneSetting : MonoBehaviour
{
    void Awake()
    {
        // QualitySettings.vSyncCount = 0;
        // Screen.fullScreen = false;
        Application.targetFrameRate = 60;
        // Screen.SetResolution((int)Def.resolution.x, (int)Def.resolution.y, false, 60);
    }
}
