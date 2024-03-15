using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTicketerInteractable : Interactable {
    public DialogueAsset DialogueAsset;
    public DialogueAsset DialogueAsset2;
    public ItemAsset ItemAsset;
    private DialogueController _dialogueController;
    private CameraController _cameraController;
    private InventoryController _inventoryController;
    private bool introduced; 
    public GameObject wall;

    void Start() {
        _dialogueController = FindObjectOfType<DialogueController>();
        _inventoryController = FindObjectOfType<InventoryController>();
        _cameraController = GetComponentInChildren<CameraController>();
     
   
    }

    protected override void OnInteract(GameObject source) {
        if (!CheckKey()) {
            ShowDialogue(); }
        else {
            UnlockTrain();
        }
        
    }

    private void ShowDialogue() {
            _dialogueController.addDialogue(DialogueAsset, _cameraController);
        
    }

    private void UnlockTrain() {
        _inventoryController.Remove(ItemAsset);
        _dialogueController.addDialogue(DialogueAsset2, _cameraController); 
        Destroy(wall);
    }

    private bool CheckKey() {
        return _inventoryController.Has(ItemAsset);
    }
}