using UnityEngine;

[CreateAssetMenu(menuName = "My assets/Dialogue", order = 100)]
public class DialogueAsset : ScriptableObject {
    public int id;
    [TextArea] public string[] dialogue;
}