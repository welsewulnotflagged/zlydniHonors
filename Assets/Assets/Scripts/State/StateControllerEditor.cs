using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(StateController))]
public class StateControllerEditor : Editor {
    // private SerializedProperty states;
    // private void OnEnable() {
    // states = serializedObject.FindProperty("states");
    // }

    public override void OnInspectorGUI() {
        var states = ((StateController)target).states;

        foreach (var state in states.Values) {
            if (state is BooleanState) {
                EditorGUILayout.Toggle(state.GetId(), (bool)state.GetValue());
            }
        }
    }
}