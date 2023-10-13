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
        _inventoryController = FindObjectOfType<InventoryController>();
        _cameraController = GetComponentInChildren<CameraController>();
    }

    protected override void OnInteract(GameObject source) {
        if (!CheckTicket()) {
            ShowDialogue();
        } else {
            UnlockTrain();
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
        _inventoryController.Remove(ItemAsset);
        Destroy(gameObject);
    }

    private bool CheckTicket() {
        return _inventoryController.Has(ItemAsset);
    }
}