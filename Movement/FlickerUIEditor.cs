
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
// using Unity.EditorCoroutines.Editor;

[CustomEditor(typeof(FlickerUI))]
[CanEditMultipleObjects]
public class FlickerUIEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     base.OnInspectorGUI();
    //     if (GUILayout.Button("Test Play"))
    //     {
    //         var t = target as FlickerUI;
    //         EditorCoroutineUtility.StartCoroutine(t.Text_Co_Flickering(), this);
    //     }
    // }
}
#endif