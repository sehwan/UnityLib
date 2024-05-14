using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByAccel : MonoBehaviour
{
    Vector2 origin;

    //setting values
    public bool isX, isY;
    public float min_x, max_x; //will be modified with origin;
    public float min_y, max_y; //will be modified with origin;
    public float speed;

    //using values
    float x, y;
    Transform tr;
    RectTransform rect;


    void Awake()
    {
        tr = transform;
        rect = tr.GetComponent<RectTransform>();
        // origin = tr.localPosition;
        origin = rect.anchoredPosition;
        min_x += origin.x;
        max_x += origin.x;
        min_y += origin.y;
        max_y += origin.y;
        x = origin.x;
        y = origin.y;
    }

    void OnEnable()
    {
        // tr.localPosition = origin;
        rect.anchoredPosition = origin;
    }
    void Update()
    {
        if (Application.isMobilePlatform == false) return;
        if (isX)
        {
            // x = Mathf.Clamp(tr.localPosition.x + (Input.acceleration.x * Time.deltaTime * speed),
            x = Mathf.Clamp(rect.anchoredPosition.x + (Input.acceleration.x * Time.deltaTime * speed),
                min_x, max_x);
        }
        if (isY)
        {
            // y = Mathf.Clamp(tr.localPosition.y + (Input.acceleration.y * Time.deltaTime * speed),
            y = Mathf.Clamp(rect.anchoredPosition.y + (Input.acceleration.y * Time.deltaTime * speed),
                min_y, max_y);
        }
        // tr.localPosition = new Vector3(x, y, 0);
        rect.anchoredPosition = new Vector3(x, y, 0);
    }
}
