using UnityEngine;

public class Inspectable : Interactable {
    public DialogueAsset DialogueAsset;

    protected override void OnInteract(GameObject source) {
        var dialogueController = FindObjectOfType<DialogueController>();
        var journalController = FindObjectOfType<JournalController>(); 
        var cameraController = GetComponentInChildren<CameraController>();

        if (dialogueController.HasActiveDialogue()) {
            dialogueController.UpdateState();
        } else {
            dialogueController.addDialogue(DialogueAsset, cameraController);
        }
    }
}