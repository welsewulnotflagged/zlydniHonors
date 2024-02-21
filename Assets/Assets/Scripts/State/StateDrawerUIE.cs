using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// [CustomPropertyDrawer(typeof(State), true)]
public class StateDrawerUIE :PropertyDrawer{
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        var root = new VisualElement();

        root.Add(new PropertyField(property.FindPropertyRelative("ID")));
        root.Add(new PropertyField(property.FindPropertyRelative("Value")));
        
        return root;
    }
}