using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/JournalEntry", order = 100)]
public class JournalAsset : ScriptableObject
{
    public string id;
    public static string entryTitle;

    [System.Serializable]
    private class SubEntry
    {
        public string content;
        public string dependency; // Only display this entry if the ID
                                  // contained in this field is not empty and is present
                                  // In the player's choicesmade list.
    }

    [TextArea] public string[] entryContent;
    public List<Choice> choices;

    [System.Serializable]
    public class Choice
    {
        public string id;
        public string choiceText;
        public string nextEntryID;
        public string TriggerState;
        public bool SaveState;
    }
}