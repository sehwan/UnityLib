using UnityEngine;
using UnityEngine.Events;

public class CollisionMessenger : MonoBehaviour
{
    public UnityEvent<Collision2D> onEnter;

    void OnCollisionEnter2D(Collision2D col)
    {
        onEnter.Invoke(col);
    }
}
