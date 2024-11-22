using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform tr;
    public Transform target;


    void Update()
    {
        if (target)
            tr.position = target.position;
    }
}
