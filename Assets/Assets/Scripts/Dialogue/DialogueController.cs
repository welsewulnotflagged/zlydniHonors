using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    public GameObject dialogueBar;
    public TMP_Text mainTextBox;
    public Queue<string> queue = new Queue<string>();

    
    
    public void addDialogue(DialogueAsset dialogueAsset) {
        foreach (var t in dialogueAsset.dialogue) {
            queue.Enqueue(t);
        }

        if (!dialogueBar.activeSelf) {
            dialogueBar.SetActive(true);
            UpdateState();
        }
    }

    private void UpdateState() {
        if (queue.Count > 0) {
            mainTextBox.text = queue.Dequeue();
        } else {
            dialogueBar.SetActive(false);
        }
    }

    public void Update() {
        if (Input.GetKeyUp(KeyCode.F)) {
            UpdateState();
        }
    }
}