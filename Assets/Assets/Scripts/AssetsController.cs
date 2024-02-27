using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetsController : MonoBehaviour {
    private Dictionary<string, DialogueAsset> _dialogues = new();
    private Dictionary<string, DialogueChoice> _dialogueChoices = new();
    private Dictionary<string, JournalAsset> _journals = new();
    private Dictionary<string, JournalAsset.Choice> _journalChoices = new();
    public bool LogAssets = false;


    private void Start() {
        Debug.Log("INIT ASSET LOADING");
        _dialogues = FindAllByType<DialogueAsset>("DialogueAssets", asset => asset.id);
        _dialogueChoices = _dialogues.Values
            .SelectMany(dialogue => dialogue.choices)
            //FOR DEBUG
            // .Select(choice => {
            //         Debug.Log($"LOADING CHOICE ID {choice.ID}");
            //         return choice;
            // })
            .ToDictionary(choice => choice.ID, choice => choice);


        _journals = FindAllByType<JournalAsset>("JournalEntries", asset => asset.id);
        _journalChoices = _journals.Values
            .SelectMany(journal => journal.choices)
            //FOR DEBUG
            // .Select(choice => {
            //         Debug.Log($"LOADING CHOICE ID {choice.ID}");
            //         return choice;
            // })
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

    private static Dictionary<string, T> FindAllByType<T>(string folderName, Func<T, string> idFunc) where T : ScriptableObject {
       /* return AssetDatabase
            .FindAssets($"t:{assetType}")
            .Select(assetId => {
                var asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assetId));
                if (asset is JournalAsset journalAsset) {
                    //FOR DEBUG
                     Debug.Log($"LOADING ASSET ID {journalAsset.id}");
                } else if (asset is DialogueAsset dialogueAsset) {
                    //FOR DEBUG
                    //Debug.Log($"LOADING ASSET ID {dialogueAsset.id}");
                }

                return asset;
            })
            .ToDictionary(asset => idFunc.Invoke(asset), asset => asset);*/
       T[] assets = Resources.LoadAll<T>(folderName);
       return assets.ToDictionary(asset => idFunc.Invoke(asset), asset => asset);
    }
}