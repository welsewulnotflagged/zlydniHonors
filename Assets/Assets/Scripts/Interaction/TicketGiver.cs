using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketGiver : Interactable
{   
    public GameObject ticket; 
    public Transform spawnPos;
    public DialogueAsset DialogueAsset;

    // Start is called before the first frame update
    protected override void OnInteract(GameObject source) 
    {
        var dialogueController = FindObjectOfType<DialogueController>();
        var cameraController = GetComponentInChildren<CameraController>();

        if (dialogueController.HasActiveDialogue()) {
            dialogueController.UpdateState();
        } else {
            dialogueController.addDialogue(DialogueAsset, cameraController);
            dialogueController.SetCallback(()=>{var ticketObject = Instantiate(ticket);
        ticketObject.transform.position = spawnPos.position; });
        }

        
    }
}
