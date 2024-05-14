using UnityEngine;
using System;

public class ProjectileCurved2D : MonoBehaviour
{
    Transform tr;
    Collider2D coll;

    [Header("Settings")]
    public float speed = 1.7f;
    public float addY = 0.2f;
    public bool willLook = true;
    public float collisioningLife = 0.85f;
    [Range(0, 1)] public float humanizing = 0;

    [Space]
    public Vector2 from;
    public Vector2 to;
    public float remainDist;
    public float totalDist;
    public float life;
    Action onArrived;


    void Awake()
    {
        tr = transform;
        coll = GetComponent<Collider2D>();
    }

    public void Init(Vector2 dest, Action onArrived)
    {
        from = tr.position;
        to = (humanizing != 0) ? tr.position.HumanizedTrajectoryDest(dest, humanizing) : dest;
        this.onArrived = onArrived;

        totalDist = Vector2.Distance(tr.position, dest);
        remainDist = totalDist;
        life = 0;
    }


    void Update()
    {
        from = Vector2.MoveTowards(from, to, Time.deltaTime * speed);
        remainDist = Vector2.Distance(from, to);

        // Curve
        life = (totalDist - remainDist) / totalDist;
        float y = Mathf.Sin(life * Mathf.PI) * totalDist * addY;
        if (float.IsNaN(y)) return;
        tr.position = new Vector2(from.x, from.y + y);

        // Rotate 
        if (willLook)
            tr.Look2D(new Vector3(to.x, to.y + remainDist - y));

        // collider on & off
        coll.enabled = life > collisioningLife;

        if (remainDist == 0 &&
            onArrived != null)
        {
            onArrived();
            onArrived = null;
        }
    }
}