using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DialogueWithTriggerInteractable : Interactable {
    public string triggerToStart;
    public string triggerToNotStart;
    protected DialogueAsset _defaultDialogue;
    protected DialogueController _dialogueController;
    protected AssetsController _assetsController;
    protected StateController _stateController;


    // DO NOT FORGET CALL base.Start() IN CHILD CLASS !!!!!!
    protected void Start() {
        _assetsController = FindObjectOfType<AssetsController>();
        _dialogueController = FindObjectOfType<DialogueController>();
        _stateController = FindObjectOfType<StateController>();
    }

    private bool CheckStartTriggers() {
        return (triggerToStart != null && _stateController.GetBoolState(triggerToStart)) || (triggerToNotStart != null && !_stateController.GetBoolState(triggerToNotStart));
    }

    protected abstract void OnDialogue(GameObject source);

    protected override void OnInteract(GameObject source) {
        if (_defaultDialogue == null) {
            _defaultDialogue = _assetsController.GetDialog("default");
        }

        if (CheckStartTriggers()) {
            OnDialogue(source);
        } else {
            _dialogueController.addDialogue(_defaultDialogue, null);
        }
    }
}