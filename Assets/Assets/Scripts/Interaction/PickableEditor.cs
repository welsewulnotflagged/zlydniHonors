#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pickable))]
[CanEditMultipleObjects]
public class PickableEditor : Editor {
    SerializedProperty radius;
    SerializedProperty item;
    SerializedProperty mesh;

    private void OnEnable() {
        radius = serializedObject.FindProperty("radius");
        item = serializedObject.FindProperty("item");
        mesh = serializedObject.FindProperty("mesh");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.Slider(radius, 0, 10, "Radius");
        EditorGUILayout.PropertyField(item);
        // EditorGUILayout.PropertyField(mesh);
        serializedObject.ApplyModifiedProperties();
    }


}
#endif