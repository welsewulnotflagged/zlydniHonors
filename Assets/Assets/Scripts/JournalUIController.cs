using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JournalUIController : MonoBehaviour
{
  //  public Text journalEntryText;
    public Transform choicesContainer; 
    public Button choiceButtonPrefab; 
    public Text[] choiceTextboxes;
    private JournalAsset _journalAsset;
    private JournalAsset.JournalEntry _entry;
    private bool _choiceContentAppended;
    [FormerlySerializedAs("_UIController")] public JournalController uiController;
    public void UpdateUI(int entryID, JournalAsset journalAsset)
    {
        _entry = journalAsset.journalEntries[entryID];
        Debug.Log(""+entryID);

        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }
        
        if (_entry.choices != null)
        {
            for (int i = 0; i < _entry.choices.Count; i++)
            {
                CreateChoiceButton(_entry.choices[i].choiceText, _entry.choices[i].nextEntryID, i);
                Debug.Log("button created");
            }
        }
    }
    
    private void CreateChoiceButton(string choiceText, int nextEntryID, int choiceIndex)
    {
       
        Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer);
        choiceButton.gameObject.SetActive(true);

        choiceButton.GetComponentInChildren<Text>().text = choiceText;
        
        choiceButton.onClick.AddListener(() => OnChoiceSelected(choiceIndex));

    }
    
    private void OnChoiceSelected(int currentID) //got rid of nextEntryId. put it back
    {
       
        JournalAsset.Choice selectedChoice = _entry.choices.Find(choice => choice.id == currentID);
       // Debug.Log("" + currentID);
        
        if (selectedChoice != null)
        {

            Debug.Log($"Choice selected!");

            Text choiceContentTextArea = uiController.textArea;
            choiceContentTextArea.text += string.Join("/n ", selectedChoice.choiceContent);
            Debug.Log($"Choice content: {selectedChoice.choiceContent}");
           // _choiceContentAppended = !_choiceContentAppended;

        }
        else
        {
            Debug.Log("no journal");
        }
    }
}
