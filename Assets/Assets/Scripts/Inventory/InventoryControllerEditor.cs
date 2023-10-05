using UnityEditor;

[CustomEditor(typeof(InventoryController))]
public class InventoryControllerEditor : Editor {
    public override bool RequiresConstantRepaint() {
        return true;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        var allItems = ((InventoryController)target).GetAllItems();

        EditorGUILayout.LabelField("Total count " + allItems.Count);
        EditorGUILayout.Separator();
        foreach (var item in allItems) {
            EditorGUILayout.LabelField(item.Key + " " + item.Value);
        }
    }
}