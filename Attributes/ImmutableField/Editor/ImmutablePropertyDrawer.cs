using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ImmutableAttribute))]
public class ImmutablePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var att = (ImmutableAttribute)attribute;
        
        bool enabled = Application.isPlaying ||! att.isEnabled;

        bool wasEnabled = GUI.enabled;
        GUI.enabled = !enabled;

        EditorGUI.PropertyField(position, property, label);

        GUI.enabled = wasEnabled;

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label);
    }
}