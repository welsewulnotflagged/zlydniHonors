using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "My assets/Dialogue", order = 100)]
public class DialogueAsset : ScriptableObject {
    public string id;
    [TextArea] public string[] dialogue;
    public List<DialogueChoice> choices;
    [FormerlySerializedAs("choicesTitles")] public string choicesTitle;
}