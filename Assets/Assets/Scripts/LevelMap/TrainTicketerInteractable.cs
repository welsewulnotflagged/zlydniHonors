using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTicketerInteractable : Interactable {
    public DialogueAsset DialogueAsset;
    public ItemAsset ItemAsset;
    private DialogueController _dialogueController;
    private CameraController _cameraController;
    private InventoryController _inventoryController;

    void Start() {
        _dialogueController = FindObjectOfType<DialogueController>();
        _cameraController = GetComponentInChildren<CameraController>();
        _inventoryController = GetComponentInChildren<InventoryController>();
    }

    protected override void OnInteract(GameObject source) {
        if (CheckTicket()) {
            UnlockTrain();
        } else {
            ShowDialogue();
        }
    }

    private void ShowDialogue() {
        if (_dialogueController.HasActiveDialogue()) {
            _dialogueController.UpdateState();
        } else {
            _dialogueController.addDialogue(DialogueAsset, _cameraController);
        }
    }

    private void UnlockTrain() {
        //
    }

    private bool CheckTicket() {
        // _inventoryController.HasItem(ItemAsset);
        return true;
    }
}