using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    public JournalAsset journalAsset;
    public CameraController cameraController;
    public GameObject journalUI;
    public JournalUIController _UIController;
    //public float fadeDuration = 1f;
    private int currentEntryIndex=0;
    public Text textArea;
   // public Text textAreaRight;
    public bool isOpen;

    public void Start()
    {
        this.gameObject.SetActive(false);
    }

  /*  public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentEntryIndex < journalAsset.journalEntries.Length)
            {
                // Display the next entry
                textArea.text = string.Join("\n", journalAsset.journalEntries[currentEntryIndex].entryContent);
                Debug.Log("Displaying Journal Entry: " + string.Join("\n", journalAsset.journalEntries[currentEntryIndex].entryContent));
                _UIController.UpdateUI(currentEntryIndex, journalAsset);
            
                // Increment the current entry index for the next click
                currentEntryIndex++;
            }
            else
            {
                // Handle the case when all entries have been displayed
                Debug.Log("All entries have been displayed.");
                //currentEntryIndex = 0;
            }
        }
    }*/

    public void OpenJournalMenu() 
    {
        Debug.Log(isOpen);
        
        if (!isOpen)
        {
            this.gameObject.SetActive(true);

            StartCoroutine(ShowUIAfterDelay(2f));

            textArea.text = string.Join("\n", journalAsset.journalEntries[0].entryContent[0]);
            //_UIController.UpdateUI(0, journalAsset);
           foreach (var entry in journalAsset.journalEntries)
            {
                textArea.text = string.Join("\n", entry.entryContent);
                Debug.Log(""+entry.entryContent);
                _UIController.UpdateUI(journalAsset.journalEntries.Length-1, journalAsset);
               //_currentEntry++;
            }
            cameraController.Enable(this.gameObject);
            
        } else
        {
            this.gameObject.SetActive(false);
            journalUI.SetActive(false);
        }

    }

    IEnumerator ShowUIAfterDelay (float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        journalUI.SetActive(true);
    }
    
}
