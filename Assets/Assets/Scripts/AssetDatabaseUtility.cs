using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public sealed class AssetDatabaseUtility {
    public static readonly AssetDatabaseUtility INSTANCE = new AssetDatabaseUtility();
    
    private readonly Dictionary<string, DialogueAsset> _dialogues = new();
    private readonly Dictionary<string, DialogueChoice> _dialogueChoices = new();

    private AssetDatabaseUtility() {
        _dialogues = FindAllByType<DialogueAsset>(typeof(DialogueAsset), asset => asset.id);
        _dialogueChoices = _dialogues.Values
            .SelectMany(dialogue => dialogue.choices)
            .ToDictionary(choice => choice.ID, choice => choice);
    }


    public DialogueChoice GetDialogChoice(string id) {
        return _dialogueChoices[id];
    }

    public DialogueAsset GetDialog(string id) {
        return _dialogues[id];
    }

    private Dictionary<string, T> FindAllByType<T>(Type assetType, Func<T, string> idFunc) where T : ScriptableObject {
        return AssetDatabase
            .FindAssets($"t:{assetType}")
            .Select(assetId => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assetId)))
            .ToDictionary(asset => idFunc.Invoke(asset), asset => asset);
    }
}