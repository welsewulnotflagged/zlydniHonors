using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    public JournalAsset journalAsset;
    public CameraController cameraController;
    public GameObject journalUI;
    public JournalUIController _UIController;
    //public float fadeDuration = 1f;
    //public JournalAsset currentEntry;
    public Text textArea;
    public bool isOpen;

    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    public void OpenJournalMenu() 
    {
        Debug.Log(isOpen);
        
        if (!isOpen)
        {
            this.gameObject.SetActive(true);

            StartCoroutine(ShowUIAfterDelay(2f));
            // GameObject journalObject = GameObject.FindGameObjectsWithTag("Journal");
            // DialogueController journal = FindObjectOfType<DialogueController>();
            foreach (var entry in journalAsset.journalEntries)
            {
                textArea.text = string.Join("\n", entry.entryContent);
                Debug.Log(""+entry.entryContent);
                _UIController.UpdateUI(journalAsset.journalEntries.Length-1, journalAsset);
            }
            cameraController.Enable(this.gameObject);
            
            /*   StringBuilder entriesText = new StringBuilder();
            
            for (int i = 0; i<=journalAsset.journalEntries.Length; i++)
            {
               
            //   entriesText.AppendLine(string.Join("\n", journalAsset.journalEntries[i].entryContent));
               _UIController.UpdateUI(i, journalAsset); 
               textArea.text = string.Join("\n", journalAsset.journalEntries.entryContent[i]);
                
            }
            textArea.text = entriesText.ToString();
            */
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

   /* public void addEntry(JournalAsset journalAsset, CameraController cameraController)
    {
        this.cameraController = cameraController;
      /*  foreach (var t in JournalAsset.JournalEntry.entryContent)
        {
           // queue.Enqueue(t);
        }
    }*/
}
