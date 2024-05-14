using UnityEngine;
using UnityEngine.Events;
public class TriggerMessenger : MonoBehaviour
{
    public UnityEvent<Collider2D> onEnter;

    void OnTriggerEnter2D(Collider2D col)
    {
        onEnter.Invoke(col);
    }
}