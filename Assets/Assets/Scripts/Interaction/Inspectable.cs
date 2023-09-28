﻿using UnityEngine;

public class Inspectable : Interactable {
    public DialogueAsset DialogueAsset;

    public override void Interact(GameObject source) {
        var dialogueController = FindObjectOfType<DialogueController>();
        var cameraController = GetComponentInChildren<CameraController>();

        if (dialogueController.HasActiveDialogue()) {
            dialogueController.UpdateState();
        } else {
            dialogueController.addDialogue(DialogueAsset, cameraController);
        }
    }
}