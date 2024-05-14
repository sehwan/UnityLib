using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class ClearTrailOnDisabled : MonoBehaviour
{
    TrailRenderer trailRenderer;

    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }
    void OnDisable()
    {
        trailRenderer.Clear();
    }
}
