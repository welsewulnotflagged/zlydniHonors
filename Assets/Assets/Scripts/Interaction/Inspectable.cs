using UnityEngine;

public class Inspectable : MonoBehaviour, Interactable {
    public DialogueAsset DialogueAsset;
    private CameraController CamSwitch;

    public void Interact(GameObject source) {
        FindObjectOfType<DialogueController>().addDialogue(DialogueAsset);
        CamSwitch = GetComponentInChildren<CameraController>();
        CamSwitch.CamEnable(source);
    }
}