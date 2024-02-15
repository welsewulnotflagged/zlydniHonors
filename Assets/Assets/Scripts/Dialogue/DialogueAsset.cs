using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/Dialogue", order = 100)]
public class DialogueAsset : ScriptableObject {
    public int id;
    [TextArea] public string[] dialogue;
    public List<Choice> choices;

    [System.Serializable]
    public class Choice {
        public string choiceText;
        public int nextDialogueID;
    }
}