using UnityEngine;

public class Inspectable : MonoBehaviour, Interactable {
    public DialogueAsset DialogueAsset;
    private CameraController CamSwitch;
    private DialogueController DialogueEnd;
    private PlayerContoller player;

    public void Interact(GameObject source) {
        FindObjectOfType<DialogueController>().addDialogue(DialogueAsset);
        CamSwitch = GetComponentInChildren<CameraController>();
        DialogueEnd = GetComponentInChildren<DialogueController>();
        if (DialogueEnd.queueEnd == false)
        {
            CamSwitch.CamEnable(source);
            //CamSwitch.CamDisable(source);
        }
        else if (DialogueEnd.queueEnd == true)
        {
            CamSwitch.CamDisable(source);
        }
    }
}