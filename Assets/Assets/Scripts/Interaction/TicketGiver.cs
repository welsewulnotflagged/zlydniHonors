using UnityEngine;

public class TicketGiver : DialogueWithTriggerInteractable {
    public ItemAsset ticket;
    public Transform spawnPos;
    public DialogueAsset dialogueAssetNoTicket;
    public DialogueAsset dialogueAssetTicket;
    public string stateTicket;

    private new void Start() {
        base.Start();
    }

    protected override void OnDialogue(GameObject source) {
        var cameraController = GetComponentInChildren<CameraController>();

        if (_stateController.GetBoolState(stateTicket)) {
            _dialogueController.addDialogue(dialogueAssetTicket, cameraController);
            _dialogueController.SetCallback(() => {
                var ticketObject = Instantiate(ticket.obj);
                _stateController.AddBoolState(triggerToNotStart);
                ticketObject.transform.position = spawnPos.position;
            });
        } else {
            _dialogueController.addDialogue(dialogueAssetNoTicket, cameraController);
            _dialogueController.SetCallback(() => {
                if (_stateController.GetBoolState(stateTicket)) {
                    OnInteract(gameObject); //continue dialog even when next dialog id is null
                }
            });
        }
    }
}