using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public sealed class AssetDatabaseUtility : MonoBehaviour {
    private Dictionary<string, DialogueAsset> _dialogues = new();
    private Dictionary<string, DialogueChoice> _dialogueChoices = new();
    private Dictionary<string, JournalAsset> _journals = new();
    private Dictionary<string, JournalAsset.Choice> _journalChoices = new();


    private void Start() {
        _dialogues = FindAllByType<DialogueAsset>(typeof(DialogueAsset), asset => asset.id);
        _dialogueChoices = _dialogues.Values
            .SelectMany(dialogue => dialogue.choices)
            .ToDictionary(choice => choice.ID, choice => choice);


        _journals = FindAllByType<JournalAsset>(typeof(JournalAsset), asset => asset.id);
        _journalChoices = _journals.Values
            .SelectMany(journal => journal.choices)
            .ToDictionary(choice => choice.id, choice => choice);
    }


    public DialogueChoice GetDialogChoice(string id) {
        return _dialogueChoices[id];
    }

    public DialogueAsset GetDialog(string id) {
        return _dialogues[id];
    }

    public JournalAsset GetJournalAsset(string id) {
        return _journals[id];
    }

    private Dictionary<string, T> FindAllByType<T>(Type assetType, Func<T, string> idFunc) where T : ScriptableObject {
        return AssetDatabase
            .FindAssets($"t:{assetType}")
            .Select(assetId => {
                var asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assetId));
                        if (asset is JournalAsset journalAsset) {
                                Debug.Log($"LOADING ASSET ID {journalAsset.id}");
                        } else if (asset is DialogueAsset dialogueAsset) {
                            Debug.Log($"LOADING ASSET ID {dialogueAsset.id}");
                        }
        
                        return asset;
                   })
            .ToDictionary(asset => idFunc.Invoke(asset), asset => asset);
    }
}