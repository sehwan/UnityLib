
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
// using Unity.EditorCoroutines.Editor;

[CustomEditor(typeof(FloatingObject))]
[CanEditMultipleObjects]
public class FloatingObjectEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     base.OnInspectorGUI();
    //     if (GUILayout.Button("Test Play"))
    //     {
    //         var t = target as FloatingObject;
    //         EditorCoroutineUtility.StartCoroutine(t.Co_TestPlay(), this);
    //     }
    // }
}
#endif
