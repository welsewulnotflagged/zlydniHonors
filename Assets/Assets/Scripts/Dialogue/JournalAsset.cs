using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/JournalEntry", order = 100)]
public class JournalAsset : ScriptableObject
{
    public int id;
    public static string entryTitle;
    [TextArea] public string[] entryContent;
    public List<Choice> choices;

    [System.Serializable]
    public class Choice
    {
        public string choiceText;
        public int nextEntryID;
        public int id;
       // [TextArea] public string choiceContent;
    }
}