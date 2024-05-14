using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenMarker : MonoBehaviour
{
    static Camera cam;
    static Transform player;
    public Transform target;

    [Header("Refs")]
    public Transform marker;
    public Transform spr;
    public Text txt;

    [Header("Options")]
    public float border = 50.0f;
    public float min = 0.1f;
    public float max = 0.9f;

    Vector2 dir;
    Vector3 screenPos; // must be Vector3

    void Start()
    {
        if (cam == null) cam = CameraWork.i.cam;
    }
    public void Init(Transform target)
    {
        this.target = target;
        gameObject.SetActive(true);
    }
    void LateUpdate()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }

        dir = target.position - player.position;
        screenPos = cam.WorldToViewportPoint(target.position);
        if (screenPos.x > min && screenPos.x < max && screenPos.y > min && screenPos.y < max)
        {
            marker.SetActive(false);
            return;
        }
        marker.SetActive(true);
        screenPos.x = Mathf.Clamp(screenPos.x, 0, 1);
        screenPos.y = Mathf.Clamp(screenPos.y, 0, 1);
        screenPos = cam.ViewportToScreenPoint(screenPos);
        screenPos.x = Mathf.Clamp(screenPos.x, border, Screen.width - border);
        screenPos.y = Mathf.Clamp(screenPos.y, border, Screen.height - border);

        marker.position = cam.ScreenToWorldPoint(screenPos);
        // txt.text = $"{dir.magnitude * 500:n0}m";
        spr.up = dir;
    }
}