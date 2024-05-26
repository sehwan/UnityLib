using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraWork : MonoBehaviour
{
    public static CameraWork i;
    public List<GameObject> obstructors = new();

    [HideInInspector] public Transform tr;
    [HideInInspector] public Camera cam;
    [HideInInspector] public Transform tr_cam;
    public Camera uiCam;
    public Vector3 zero = new(0, 0, -10);
    public Vector3 startingPos;
    public float startingZoom;

    [Header("Post Processing")]
    public VolumeProfile profile;

    // Editor
    Vector3 oldPos;
    Vector3 oldMouse;

    // Mobile
    Vector2?[] oldTouchPos = {
        null,
        null
    };
    Vector2 oldVector;
    float oldDistance;
    bool isOrthographic;

    void Awake()
    {
        i = this;
        tr = transform;
        cam = tr.GetChild(0).GetComponent<Camera>();
        tr_cam = cam.transform;

        startingPos = tr.position;
        isOrthographic = cam.orthographic;
        startingZoom = isOrthographic ? cam.orthographicSize : cam.fieldOfView;
        if (minimap != null)
        {
            oriSize_cam = cam.orthographicSize;
            oriSize_minimap = minimap.transform.localScale;
        }
    }
    void Start()
    {
        UM.i.onRegisterWindow += (go) => obstructors.Add(go);
        UM.i.onDeregisterWindow += (go) => obstructors.Remove(go);
    }

    void Update()
    {
        // Passive
        if (isFollowingObject) Follow();

        // Active
        if (isPan) Pan();
        if (isZoom) Zoom();
        if (isRotating) Rotate();
        if (minimap) MiniMap();
        if (isPan) Clamp();
    }
    void LateUpdate()
    {
        if (uiCam == null) return;
        if (isOrthographic) uiCam.orthographicSize = cam.orthographicSize;
        else uiCam.fieldOfView = cam.fieldOfView;
    }
    [ContextMenu("Align UI Camera")]
    void AlignUICamera()
    {
        uiCam.fieldOfView = cam.fieldOfView;
        uiCam.orthographicSize = cam.orthographicSize;
    }

    public void SetPos(Vector3 pos)
    {
        ori_pos = pos;
        tr.position = pos;
    }

    #region Follow
    [Header("Follow")]
    public bool isFollowingObject;
    public float spd_followingTarget = 1;
    public float spd_dynamicPosition = 1;
    public float spd_dynamicZoom = 1;
    // Object
    [SerializeField] Transform target;
    public void SetTarget(Transform t)
    {
        target = t;
    }
    // Position
    public Vector3? targetPos = null;
    public float? targetZoom = null;
    void Follow()
    {
        // Pan
        if (target)
        {
            var dir = spd_followingTarget * Time.deltaTime * (target.position - tr.position + zero);
            tr.Translate(dir);
        }
        else if (targetPos.HasValue)
        {
            var dir = spd_dynamicPosition * Time.deltaTime * (targetPos.Value - tr.position + zero);
            tr.Translate(dir);
        }

        // Zoom
        if (isZoom && targetZoom.HasValue)
        {
            if (isOrthographic)
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom.Value, Time.deltaTime * 0.5f);
            else
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom.Value, Time.deltaTime * 0.5f);
        }
    }
    #endregion

    #region Pan
    [Header("Pan")]
    public bool isPan;
    public bool isXMovable = true;
    public bool isYMovable = true;
    public float panSpeed;
    void Pan()
    {
        if (obstructors.Count > 0) return;
#if UNITY_EDITOR        
        Pan_EditorOrDesktop();
#else
        Pan_Mobile();
#endif
    }

    void Pan_Mobile()
    {
        if (Input.touchCount != 1)
        {
            oldTouchPos[0] = null;
            oldTouchPos[1] = null;
            return;
        }
        Vector2 newTouchPos = Input.GetTouch(0).position;
        if (oldTouchPos[0].HasValue == false) oldTouchPos[0] = newTouchPos;
        else if (Vector2.Distance(newTouchPos, oldTouchPos[0].Value) > 200) return;

        var moved = oldTouchPos[0].Value - newTouchPos;
        if (isXMovable == false) moved.x = 0;
        if (isYMovable == false) moved.y = 0;
        var spd = isOrthographic ?
            cam.orthographicSize / cam.pixelHeight * 2f :
            cam.fieldOfView;
        spd *= panSpeed;
        tr.position += tr.TransformDirection((Vector3)(spd * moved));
        oldTouchPos[0] = newTouchPos;
    }
    void Pan_EditorOrDesktop()
    {
        if (Input.GetMouseButton(0))
        {
            var newMouse = cam.ScreenToViewportPoint(Input.mousePosition);
            var moved = (oldMouse - newMouse) * panSpeed;
            if (isXMovable == false) moved.x = 0;
            if (isYMovable == false) moved.y = 0;
            tr.position = oldPos + moved * (isOrthographic ? cam.orthographicSize : cam.fieldOfView / 6);
        }
        oldPos = tr.position;
        oldMouse = cam.ScreenToViewportPoint(Input.mousePosition);
    }
    public float limit_minX = -25;
    public float limit_maxX = 25;
    public float limit_maxY = 25;
    public float limit_minY = -25;
    public void SetBoundary(float minX, float maxX, float minY, float maxY)
    {
        limit_minX = minX;
        limit_maxX = maxX;
        limit_minY = minY;
        limit_maxY = maxY;
    }
    public void SetBoundary(float n)
    {
        limit_minX = -n + zero.x;
        limit_maxX = n + zero.x;
        limit_minY = -n + zero.y;
        limit_maxY = n + zero.y;
    }
    void Clamp()
    {
        var zoom = 0f;
        if (isOrthographic)
        {
            zoom = cam.orthographicSize / 2;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
        else
        {
            zoom = cam.fieldOfView / 15;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        }
        var x = isXMovable ?
            Mathf.Clamp(tr.position.x, limit_minX + zoom, limit_maxX - zoom) :
            zero.x;
        var y = isYMovable ?
            Mathf.Clamp(tr.position.y, limit_minY + zoom, limit_maxY - zoom) :
            zero.y;
        tr.Position(x, y, zero.z);
    }
    #endregion

    #region Rotate
    [Header("Rotate")]
    public bool isRotating;
    void Rotate()
    {
        if (Input.touchCount == 2)
        {
            if (oldTouchPos[1] == null)
            {
                oldTouchPos[0] = Input.GetTouch(0).position;
                oldTouchPos[1] = Input.GetTouch(1).position;
                oldVector = (Vector2)(oldTouchPos[0] - oldTouchPos[1]);
                oldDistance = oldVector.magnitude;
            }
            else
            {
                Vector2 screen = new(cam.pixelWidth, cam.pixelHeight);

                Vector2[] newTouchPositions = {
                    Input.GetTouch(0).position,
                    Input.GetTouch(1).position
                };
                Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
                float newTouchDistance = newTouchVector.magnitude;

                if (isOrthographic)
                {
                    tr.position += tr.TransformDirection((Vector3)((oldTouchPos[0] + oldTouchPos[1] - screen) * cam.orthographicSize / screen.y));
                    tr.localRotation *= Quaternion.Euler(new Vector3(0, 0, Mathf.Asin(Mathf.Clamp((oldVector.y * newTouchVector.x - oldVector.x * newTouchVector.y) / oldDistance / newTouchDistance, -1f, 1f)) / 0.0174532924f));
                    cam.orthographicSize *= oldDistance / newTouchDistance;
                    tr.position -= tr.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * cam.orthographicSize / screen.y);
                }
                else
                {
                    tr.position += tr.TransformDirection((Vector3)((oldTouchPos[0] + oldTouchPos[1] - screen) * cam.fieldOfView / screen.y));
                    tr.localRotation *= Quaternion.Euler(new Vector3(Mathf.Asin(Mathf.Clamp((oldVector.y * newTouchVector.x - oldVector.x * newTouchVector.y) / oldDistance / newTouchDistance, -1f, 1f)) / 0.0174532924f, 0, 0));
                    cam.fieldOfView *= oldDistance / newTouchDistance;
                    tr.position -= tr.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * cam.fieldOfView / screen.y);
                }

                oldTouchPos[0] = newTouchPositions[0];
                oldTouchPos[1] = newTouchPositions[1];
                oldVector = newTouchVector;
                oldDistance = newTouchDistance;
            }
        }
    }
    #endregion

    #region Zoom
    [Header("Zoom")]
    public bool isZoom;
    public float minZoom = 3f;
    public float maxZoom = 20f;
    public float zoom_spd_scroll = 22f;
    public float zoom_spd_touch = 0.1f;
    public void SetZoom(float s)
    {
        if (isOrthographic) cam.orthographicSize = s;
        else cam.fieldOfView = s;
    }
    public void SetZoomToStarting()
    {
        SetZoom(startingZoom);
    }
    void Zoom()
    {
#if UNITY_EDITOR
        Zoom_Desktop();
#else
        Zoom_Mobile();
#endif
    }
    void Zoom_Desktop()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (isOrthographic)
                cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoom_spd_scroll;
            else
                cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoom_spd_scroll;
        }
    }
    void Zoom_Mobile()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (isOrthographic) cam.orthographicSize += deltaMagnitudeDiff * zoom_spd_touch;
            else cam.fieldOfView += deltaMagnitudeDiff * zoom_spd_touch;
        }
    }
    #endregion

    #region Shake
    [Header("Shake")]
    public bool isBackToOriginPosition;
    public Vector3 ori_pos;
    public float ori_size;

    // Pan
    Coroutine co_panShake;
    public void PanShake(float time = 1, float mag = 10)
    {
        if (co_panShake != null) StopCoroutine(co_panShake);
        co_panShake = StartCoroutine(Co_PanShake(time, mag));
    }
    IEnumerator Co_PanShake(float time, float mag)
    {
        var passed = 0f;
        var randomVector = Random.insideUnitSphere;
        while (passed < time)
        {
            var dt = Time.deltaTime;
            passed += dt;
            var prog = passed / time;
            if (passed % 0.1f < dt) // Update random vector every 0.1 seconds
                randomVector = Random.insideUnitSphere;
            tr_cam.localPosition = mag * Mathf.Log(prog, 0.5f) * dt * randomVector;
            yield return null;
        }
        co_panShake = null;
    }

    // Zoom
    Coroutine co_zoomShake;
    public void ZoomShake(float time = 1, float mag = 1)
    {
        if (co_zoomShake != null)
        {
            StopCoroutine(co_zoomShake);
            BackToOrigin();
        }
        SaveOrigin();
        co_zoomShake = StartCoroutine(Co_ZoomShake(time, mag));
    }
    IEnumerator Co_ZoomShake(float time, float mag)
    {
        float ori = isOrthographic ?
            cam.orthographicSize :
            cam.fieldOfView;
        float passed = 0;
        while (passed < time)
        {
            passed += Time.deltaTime;
            var prog = passed / time;
            if (isOrthographic)
                cam.orthographicSize = RandomEx.R(mag) * Mathf.Log(prog, 0.1f) + ori;
            else
                cam.fieldOfView = RandomEx.R(mag) * Mathf.Log(prog, 0.1f) + ori;
            yield return null;
        }
        cam.orthographicSize = ori;
        co_zoomShake = null;
    }

    // PunchSize
    Coroutine co_zoomPunch;
    public void ZoomPunch(float time = 0.1f, float mag = 0.3f)
    {
        if (isFollowingObject == false && co_zoomPunch != null)
        {
            StopCoroutine(co_zoomPunch);
            BackToOrigin();
        }
        SaveOrigin();
        co_zoomPunch = StartCoroutine(Co_ZoomPunch(time, mag));
    }
    IEnumerator Co_ZoomPunch(float time, float mag)
    {
        ori_size = cam.orthographicSize;
        float passed = 0;
        float prog = 0;
        while (passed < time)
        {
            passed += Time.deltaTime;
            prog = passed / time;
            cam.orthographicSize = ori_size - (mag * Mathf.Log(prog, .1f));
            yield return null;
        }

        float after = cam.orthographicSize;
        time *= 2;
        passed = 0;
        while (passed < time)
        {
            passed += Time.deltaTime;
            prog = passed / time;
            cam.orthographicSize = after + mag * Mathf.Log(prog, .1f);
            yield return null;
        }
        cam.orthographicSize = ori_size;
        co_zoomPunch = null;
    }

    void SaveOrigin()
    {
        ori_size = cam.orthographicSize;
    }
    void BackToOrigin()
    {
        cam.orthographicSize = ori_size;
        if (isBackToOriginPosition) tr_cam.localPosition = new Vector3();
    }
    #endregion

    #region Minimap
    [Header("MiniMap")]
    public bool isMinimap;
    public SpriteRenderer minimap;
    public float oriSize_cam;
    public Vector2 oriSize_minimap;
    void MiniMap()
    {
        minimap.transform.localScale = oriSize_minimap / (oriSize_cam / cam.orthographicSize);
    }
    #endregion
}