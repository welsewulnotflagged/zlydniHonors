using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JournalUIController : MonoBehaviour {
    public Transform choicesContainer;
    public Button choiceButtonPrefab;
    public Text[] choiceTextboxes;
    private JournalAsset _journalAsset;
    private bool _choiceAppended;
    private Text textArea;
    public JournalAsset nextEntry;
    private StateController _stateController;

    [FormerlySerializedAs("_UIController")]
    public JournalController uiController;

    private void Start() {
        _stateController = FindObjectOfType<StateController>();
    }

    public void UpdateUI(JournalAsset selectedEntry) {
        _journalAsset = selectedEntry;
        // _entry = _journalAsset.journalEntries[entryID];


        foreach (Transform child in choicesContainer) {
            Destroy(child.gameObject);
        }

        if (_journalAsset.choices != null) {
            for (int i = 0; i < _journalAsset.choices.Count; i++) {
                CreateChoiceButton(_journalAsset.choices[i]);
                Debug.Log("button created");
            }
        }
    }

    private void CreateChoiceButton(JournalAsset.Choice choiceInfo) {
        if (!string.IsNullOrEmpty(choiceInfo.TriggerState) && !_stateController.GetBoolState(choiceInfo.TriggerState)) {
            Debug.Log($"SKIP journal choice {choiceInfo.id} because of {choiceInfo.TriggerState}");
            uiController.waitingForButton = true;
            return;
        }

        Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer);
        choiceButton.gameObject.SetActive(true);

        choiceButton.GetComponentInChildren<Text>().text = choiceInfo.choiceText;

        choiceButton.onClick.AddListener(() => {
            // choicesMade = JournalUIController.Instance.choicesMade;
            // JournalAsset.Choice newChoice = new JournalAsset.Choice();
            // newChoice.choiceText = "";
            // newChoice.id = "ActionEscape-Success";

            // choicesMade.Add(choiceInfo);
            if (choiceInfo.SaveState) {
                _stateController.AddBoolState(choiceInfo);
            }// this is for save in inspector lol
            
            // if trigger from dialogue is triggered then create more buttons from respective journal asset
            

            OnChoiceSelected(choiceInfo.nextEntryID);
        });
    }

    public void OnChoiceSelected(string nextEntryID) {
        // Ensure this entry is only appended once.
        _choiceAppended = !_choiceAppended;

        // if (!_choiceAppended)
        // {
        //     JournalAsset.Choice selectedChoice = _journalAsset.choices.Find(choice => choice.id == currentID);
        //
        //     if (selectedChoice != null)
        //     {
        //
        //         Debug.Log($"Choice selected!");
        //         Debug.Log($"Choice content: {selectedChoice.choiceText}");
        //         currentID = selectedChoice.nextEntryID;
        //          // check this
        //     }
        //     else
        //     {
        //         Debug.Log("no journal");
        //     }
        // }
        // else 
        if (_choiceAppended) {
            Debug.Log("Choice already selected. Should display next entry");

            nextEntry = AssetDatabaseUtility.INSTANCE.GetJournalAsset(nextEntryID);

            if (nextEntry != null) {
                Text choiceContentTextArea = uiController.textArea;
                UpdateUI(nextEntry);
                uiController.waitingForButton = false;
                foreach (Transform child in choicesContainer) {
                    Destroy(child.gameObject);
                }
            } else {
                Debug.Log("no entry available");
            }
        }
    }
}