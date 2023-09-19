using UnityEngine;

public class Inspectable : MonoBehaviour, Interactable {
    public DialogueAsset DialogueAsset;

    public void Interact(GameObject source) {
        FindObjectOfType<DialogueController>().addDialogue(DialogueAsset);
    }
}