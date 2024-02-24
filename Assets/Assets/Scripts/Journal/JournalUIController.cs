using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JournalUIController : MonoBehaviour {
    public Transform choicesContainer;
    public Button choiceButtonPrefab;
    public Text[] choiceTextboxes;
    private JournalAsset _journalAsset;
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
            
            if (choiceInfo.SaveState) {
                _stateController.AddBoolState(choiceInfo);
            } // this is for save in inspector lol

            // if trigger from dialogue is triggered then create more buttons from respective journal asset
            
            OnChoiceSelected(choiceInfo.nextEntryID);
        });
    }

    public void OnChoiceSelected(string nextEntryID) {
        Debug.Log("Choice already selected. Should display next entry");

        nextEntry = AssetDatabaseUtility.INSTANCE.GetJournalAsset(nextEntryID);

        if (nextEntry != null) {
            uiController.waitingForButton = false;
            uiController.HandleClick();
            foreach (Transform child in choicesContainer) {
                Destroy(child.gameObject);
            }
        } else {
            Debug.Log("no entry available");
        }
    }
}