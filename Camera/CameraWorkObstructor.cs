using UnityEngine;

public class CameraWorkObstructor : MonoBehaviour
{
    void OnEnable()
    {
        CameraWork.i.obstructors.Add(gameObject);
    }
    void OnDisable()
    {
        CameraWork.i.obstructors.Remove(gameObject);
    }
}
