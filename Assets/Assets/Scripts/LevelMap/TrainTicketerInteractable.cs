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
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject wall;

    void Start() {
        _dialogueController = FindObjectOfType<DialogueController>();
        _inventoryController = FindObjectOfType<InventoryController>();
        _cameraController = GetComponentInChildren<CameraController>();
        enemy1.SetActive(false);
        enemy2.SetActive(false);
    }

    protected override void OnInteract(GameObject source) {
        if (!CheckTicket()) {
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
        enemy1.SetActive(true);
        enemy2.SetActive(true);
        enemy3.SetActive(true);
        Destroy(wall);
    }

    private bool CheckTicket() {
        return _inventoryController.Has(ItemAsset);
    }
}