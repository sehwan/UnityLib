using UnityEngine;

public class CameraWorkObstructor : MonoBehaviour
{
    void OnEnable()
    {
        if (CameraWork.i == null) return;
        CameraWork.i.obstructors.Add(gameObject);
    }
    void OnDisable()
    {
        if (CameraWork.i == null) return;
        CameraWork.i.obstructors.Remove(gameObject);
    }
}
