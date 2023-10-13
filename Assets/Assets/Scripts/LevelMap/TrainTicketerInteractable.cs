using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrainTicketerInteractable : Interactable {
    public DialogueAsset DialogueAsset;
    public ItemAsset ItemAsset;
    public Transform Destination;
    private DialogueController _dialogueController;
    private CameraController _cameraController;
    private NavMeshAgent _navMeshAgent;
    private InventoryController _inventoryController;

    void Start() {
        _dialogueController = FindObjectOfType<DialogueController>();
        _cameraController = GetComponentInChildren<CameraController>();
        _inventoryController = GetComponentInChildren<InventoryController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
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
            _dialogueController.SetCallback(() => {
                _navMeshAgent.SetDestination(Destination.position);
            });
        }
    }

    private void UnlockTrain() {
        //
    }

    private bool CheckTicket() {
        // _inventoryController.HasItem(ItemAsset);
        return false;
    }
}