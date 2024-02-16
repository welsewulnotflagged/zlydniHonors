using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "My assets/Dialogue", order = 100)]
public class DialogueAsset : ScriptableObject {
    public int id;
    [TextArea] public string[] dialogue;
    public List<Choice> choices;
    [FormerlySerializedAs("choicesTitles")] public string choicesTitle;

    [System.Serializable]
    public class Choice {
        public string choiceText;
        public int nextDialogueID;
    }
}