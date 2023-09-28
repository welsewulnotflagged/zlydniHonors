using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    public GameObject dialogueBar;
    public TMP_Text mainTextBox;
    public Queue<string> queue = new();
    private CameraController cameraController;


    public void addDialogue(DialogueAsset dialogueAsset, CameraController cameraController) {
        this.cameraController = cameraController;
        foreach (var t in dialogueAsset.dialogue) {
            queue.Enqueue(t);
        }

        if (!dialogueBar.activeSelf) {
            dialogueBar.SetActive(true);
            UpdateState();
        }
    }

    public void UpdateState() {
        Debug.Log("update state");
        var player = FindObjectOfType<PlayerContoller>();
        if (queue.Count > 0) {
            Debug.Log("start dialog");
            mainTextBox.text = queue.Dequeue();
            if (cameraController) {
                cameraController.Enable(player.gameObject);
            }
        } else {
            Debug.Log("close dialog");
            dialogueBar.SetActive(false);
            if (cameraController) {
                cameraController.Disable(player.gameObject);
            }
        }
    }

    public bool HasActiveDialogue() {
        return dialogueBar.activeSelf;
    } 

    public void Update() {
        if (Input.GetMouseButtonUp(0)) {
            UpdateState();
        }
    }
}