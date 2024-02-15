using System;
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


    private void Start() {
        _uiController = FindObjectOfType<UIController>();
    }

    public void addDialogue(DialogueAsset dialogueAsset, CameraController cameraController) {
        this.cameraController = cameraController;
        foreach (var t in dialogueAsset.dialogue)
        {
            queue.Enqueue(t);
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
        Debug.Log("update state");
        var player = FindObjectOfType<PlayerController>();
        if (queue.Count > 0)
        {
            Debug.Log("start dialog");
            _uiController.SetDialogueText(queue.Dequeue());
            if (cameraController)
            {
                cameraController.Enable(player.gameObject);
            }
        }
        else 
        {
            Debug.Log("" + _uiController._dialogue.childCount);
            //var dialogueCopy = new List<DialogueAsset.Choice>(_dialogueAsset.choices);
            if (_dialogueAsset.choices is { Count: > 0 } && _uiController.ButtonContainer.childCount == 0 )
            {
                Debug.Log("IM IN HERE");
               // _uiController._dialogue.Clear();
                //foreach (var choice in _dialogueAsset.choices)
                for (int i = 0; i < _dialogueAsset.choices.Count; i++)
                { 
                   // _uiController.SetDialogueText(_dialogueAsset.choices[i].choiceText);
                    _uiController.AddButton(_dialogueAsset.choices[i], i); 
                    
                   // _dialogueAsset.choices.Clear();
                }
                return;
            }

            Debug.Log("close dialog");
                _uiController.ShowHUD();
                if (cameraController)
                {
                    cameraController.Disable(player.gameObject);
                }

                if (_dialogueCallback != null)
                {
                    _dialogueCallback.Invoke();
                    _dialogueCallback = null;
                }
            
        }
    }

    public bool HasActiveDialogue() {
        return _uiController.IsDialogueActive();
    } 

    public void Update() {
        if (_uiController.IsDialogueActive() && Input.GetMouseButtonUp(0)) 
            UpdateState();
        
    }
    
}