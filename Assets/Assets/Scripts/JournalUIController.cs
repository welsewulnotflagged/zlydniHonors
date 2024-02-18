using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JournalUIController : MonoBehaviour
{
    public Transform choicesContainer; 
    public Button choiceButtonPrefab; 
    public Text[] choiceTextboxes;
    private JournalAsset _journalAsset;
    private bool _choiceAppended;
    private Text textArea;
    public JournalAsset nextEntry;
    [FormerlySerializedAs("_UIController")] public JournalController uiController;
    
    public void UpdateUI(JournalAsset selectedEntry)
    {
        _journalAsset = selectedEntry;
       // _entry = _journalAsset.journalEntries[entryID];
       
           
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }

        if (_journalAsset.choices != null)
        {
            for (int i = 0; i < _journalAsset.choices.Count; i++)
            {
                CreateChoiceButton(_journalAsset.choices[i]);
                Debug.Log("button created");
            }
        }
    }
    
    private void CreateChoiceButton(JournalAsset.Choice choiceInfo)
    {
       
        Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer);
        choiceButton.gameObject.SetActive(true);

        choiceButton.GetComponentInChildren<Text>().text = choiceInfo.choiceText;
        
        choiceButton.onClick.AddListener(() => OnChoiceSelected(choiceInfo.nextEntryID));

    }
    
    public void OnChoiceSelected(int currentID) 
    {
        _choiceAppended = !_choiceAppended;
        
        if (!_choiceAppended)
        {
            JournalAsset.Choice selectedChoice = _journalAsset.choices.Find(choice => choice.id == currentID);

            if (selectedChoice != null)
            {

                Debug.Log($"Choice selected!");
                Debug.Log($"Choice content: {selectedChoice.choiceText}");
                currentID = selectedChoice.nextEntryID;
                 // check this
                
            }
            else
            {
                Debug.Log("no journal");
            }
        }
        else if (_choiceAppended)
        {
            Debug.Log("Choice already selected. Should display next entry");
            
            nextEntry = FindEntryByID(currentID);
            
            if (nextEntry != null)
            {
                Text choiceContentTextArea = uiController.textArea;
                UpdateUI(nextEntry);
                foreach (Transform child in choicesContainer)
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                Debug.Log("no entry available");
            }
        }
    }

    private JournalAsset FindEntryByID(int entryID)
    {
        var nextEntries =
            AssetDatabase
                .FindAssets($"t:{typeof(JournalAsset)}")
                .Select(assetId => AssetDatabase.LoadAssetAtPath<JournalAsset>(AssetDatabase.GUIDToAssetPath(assetId)))
                .Where(asset => asset.id == entryID)
                .ToList();

        switch (nextEntries.Count)
        {
            case > 1:
                Debug.LogError("LOLITAAA!!!!!!!!!!! FIX YOUR JOURNAL :D IDS");
                foreach (var journalAsset in nextEntries)
                {
                    Debug.Log("haiii im a duplicate" + journalAsset.id);
                }

                return null;
            case 0:
                Debug.LogError($"CAN'T FIND DIALOGUE WITH ID {entryID}");
                return null;
            default:
                Debug.Log($"SWITCH TO NEXT DIALOGUE WITH ID {entryID}");
               // _journalAsset.addDialogue(nextDialogues.First(), _dialogueController.GetActiveCamera());

                return nextEntries.First();
        }
    }
    
}
