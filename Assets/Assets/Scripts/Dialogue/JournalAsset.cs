using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/JournalEntry", order = 100)]
public class JournalAsset : ScriptableObject
{
    [System.Serializable]
    public class JournalEntry
    {
        public int id;
        public static string entryTitle;
        [TextArea] public string[] entryContent;
        public List<Choice> choices;
    }

    [System.Serializable]
    public class Choice
    {
        public string choiceText;
        public int nextEntryID;
        public int id;
        [TextArea] public string choiceContent;
    }
    
    public JournalEntry[] journalEntries;  // Make sure this is declared as public

}