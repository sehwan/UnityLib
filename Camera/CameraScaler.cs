using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    Camera m_Camera;
    Camera Camera { get { return m_Camera != null ? m_Camera : m_Camera = GetComponent<Camera>(); } }

    public double CameraTargetWidth = 7.2f;

    int m_CurrentScreenWidth = -1;
    int m_CurrentScreenHeight = -1;

    public bool IsCameraChanged
    {
        get
        {
            var value = Screen.width != m_CurrentScreenWidth || Screen.height != m_CurrentScreenHeight;
            if (value)
            {
                m_CurrentScreenWidth = Screen.width;
                m_CurrentScreenHeight = Screen.height;
            }
            return value;
        }
    }

    void Update() { if (IsCameraChanged) FitCamera(); }
    void OnValidate() { FitCamera(); }


    void FitCamera()
    {
        Camera.orthographicSize = (float)(CameraTargetWidth / Camera.aspect * .5f);
    }
}