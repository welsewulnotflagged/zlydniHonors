﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    public Queue<string> queue = new();
    private CameraController cameraController;
    private Action _dialogueCallback;
    private DialogueAsset _dialogueAsset;
    private UIController _uiController;
    private JournalUIController _journalUIController;


    private void Start() {
        _uiController = FindObjectOfType<UIController>();
    }

    public void addDialogue(DialogueAsset dialogueAsset, CameraController cameraController)
    {
        this.cameraController = cameraController;
        foreach (var t in dialogueAsset.dialogue)
        {
            queue.Enqueue(t);
        }

        if (JournalUIController.Instance.choicesMade.Exists(choice => choice.id == "IdToSearchFor"))
        {
            // This choice id was selected by the player
            Debug.Log("Found");
        }


    _dialogueAsset = dialogueAsset;


        if (!_uiController.IsDialogueActive()) {
            _uiController.ShowDialogue();
            UpdateState();
        }
    }

    public void SetCallback(Action callback) {
        _dialogueCallback = callback;
    }

    public void UpdateState() {
        var player = FindObjectOfType<PlayerController>();
        if (queue.Count > 0) {
            _uiController.ClearDialogueButtons();
            _uiController.SetDialogueText(queue.Dequeue());
            if (cameraController) {
                cameraController.Enable(player.gameObject);
            }
            
            if (queue.Count == 0 && _dialogueAsset.choices is { Count: > 0 } && !_uiController.HasActiveChoices()) {
                _uiController.ShowChoices(_dialogueAsset);
            }
            
            return;
        }
        
        

        Debug.Log("close dialog");
        _uiController.ShowHUD();
        if (cameraController) {
            cameraController.Disable(player.gameObject);
        }

        if (_dialogueCallback != null) {
            _dialogueCallback.Invoke();
            _dialogueCallback = null;
        }
    }


    public bool HasActiveDialogue() {
        return _uiController.IsDialogueActive();
    }

    public void Update() {
        if (_uiController.IsDialogueActive() && Input.GetMouseButtonUp(0) && !_uiController.HasActiveChoices())
            UpdateState();
    }

    public CameraController GetActiveCamera() {
        return cameraController;
    }
}