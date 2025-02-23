using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CanvasAutoMainCameraAssigner))]
public class CanvasAutoMainCameraAssignerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CanvasAutoMainCameraAssigner myScript = (CanvasAutoMainCameraAssigner)target;
        Canvas canvas = myScript.GetComponent<Canvas>();
        // Debug.Log($"<color=green>{canvas.renderMode} {canvas.worldCamera}</color>");
        if (canvas.renderMode != RenderMode.WorldSpace && canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
            EditorUtility.SetDirty(canvas);
        }
    }
}