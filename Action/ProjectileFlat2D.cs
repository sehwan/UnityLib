using UnityEngine;
using System;

public class ProjectileFlat2D : MonoBehaviour
{
    [Header("Settings")]
    Transform tr;
    public float speed = 2.2f;
    public bool willLook = true;
    [Range(0, 1)] public float humanizing = 0;

    [Space]
    public Vector2 dest;
    Action onArrived;


    void Awake()
    {
        tr = transform;
    }
    public void Init(Vector2 dest, Action onArrived)
    {
        this.dest = tr.position.HumanizedTrajectoryDest(dest, humanizing);
        this.onArrived = onArrived;

        if (willLook)
        {
            LookAt();
            this.InvokeEx(LookAt, 0.05f);
        }
    }
    void LookAt()
    {
        tr.Look2D(dest - tr.position.V2());
    }


    void Update()
    {
        if (tr.position.V2() == dest)
        {
            if (onArrived != null) onArrived();
        }
        tr.position = Vector2.MoveTowards(tr.position, dest, Time.deltaTime * speed);
    }
}
