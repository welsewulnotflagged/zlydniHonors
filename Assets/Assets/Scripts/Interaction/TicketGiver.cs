using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TicketGiver : Interactable {
    public GameObject ticket;
    public Transform spawnPos;
    public DialogueAsset dialogueAssetNoTicket;
    public DialogueAsset dialogueAssetTicket;
    public string stateTicket;
    public bool spawnedTicket;

    private DialogueController _dialogueController;
    private StateController _stateController;

    private void Start() {
        _dialogueController = FindObjectOfType<DialogueController>();
        _stateController = FindObjectOfType<StateController>();
    }

    protected override void OnInteract(GameObject source) {
        var dialogueController = _dialogueController;
        var cameraController = GetComponentInChildren<CameraController>();

        if (_stateController.GetBoolState(stateTicket)) {
            if (!spawnedTicket) {
                dialogueController.addDialogue(dialogueAssetTicket, cameraController);
                dialogueController.SetCallback(() => {
                    var ticketObject = Instantiate(ticket);
                    ticketObject.transform.position = spawnPos.position;
                    spawnedTicket = true;
                });
            }
        } else {
            dialogueController.addDialogue(dialogueAssetNoTicket, cameraController);
            dialogueController.SetCallback(() => {
                if (_stateController.GetBoolState(stateTicket)) {
                    OnInteract(gameObject); //continue dialog even when next dialog id is null
                }
            });
        }
    }
}