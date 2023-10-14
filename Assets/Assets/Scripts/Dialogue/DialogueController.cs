using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    public Queue<string> queue = new();
    private CameraController cameraController;
    private Action _dialogueCallback;
    
    private UIController _uiController;


    private void Start() {
        _uiController = FindObjectOfType<UIController>();
    }

    public void addDialogue(DialogueAsset dialogueAsset, CameraController cameraController) {
        this.cameraController = cameraController;
        foreach (var t in dialogueAsset.dialogue) {
            queue.Enqueue(t);
        }

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
        var player = FindObjectOfType<PlayerContoller>();
        if (queue.Count > 0) {
            Debug.Log("start dialog");
            _uiController.SetDialogueText(queue.Dequeue());
            if (cameraController) {
                cameraController.Enable(player.gameObject);
            }
        } else {
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
    }

    public bool HasActiveDialogue() {
        return _uiController.IsDialogueActive();
    } 

    public void Update() {
        if (_uiController.IsDialogueActive() && Input.GetMouseButtonUp(0)) {
            UpdateState();
        }
    }
}