using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform tr;
    public Transform target;


    void Update()
    {
        if (target && target.gameObject.activeSelf)
            tr.position = target.position;
    }
}
