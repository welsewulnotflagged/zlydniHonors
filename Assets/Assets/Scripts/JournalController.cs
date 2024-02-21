using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JournalController : MonoBehaviour
{
    // 800 characters of journal content
    // You only display 2 * max character count ata  time
    // And then offset the start of that substring by currentPage * 2*maxCharacter
    // Only display the next button if there is more text than is displayed by current page
    // You can calculate the current text every time you display the journal
    // As the entries might be added from anywhere. 
    // The content of the journal is now a string, only a 2 * max character is displayed at any time.
    //public JournalAsset journalAsset;
    public CameraController cameraController;
    public GameObject journalUI;

    public JournalUIController _UIController;

    public Button next;
    public Button previous;

    //public float fadeDuration = 1f;
    int clickedEntryIndex = 0;
    public int currentPage = 0;
    public string journalContent;

    public Text textArea;

    // public Text textAreaRight;
    public bool isOpen;
    private bool allowPlayerInput;

    public void Start()
    {
        
     //   this.gameObject.SetActive(false);
     next.onClick.AddListener(() => ChangePage(1));
     
     previous.onClick.AddListener(() => ChangePage(-1));
    }

    public void ChangePage(int page)
    {
        if (page < 0)
        {
            if (currentPage > 0)
            {
                // Update page offset
                currentPage += page;
            }
        }
        else
        {
            // The total number of current pages equals the  
            if (currentPage < Mathf.Ceil(journalContent.Length / TextOverflowCheck.maxCharacterCount*2))
            {
                currentPage += page;
            }
        }
    }
    public void Update()
    {
        if (allowPlayerInput)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && _UIController.choicesContainer.childCount == 0)
        {
            JournalAsset _foundJournalAsset;

            _foundJournalAsset = _UIController.nextEntry != null ? _UIController.nextEntry : FindEntryByID(0);
            TextOverflowCheck textChecker = FindObjectOfType<TextOverflowCheck>();


            if (_foundJournalAsset != null)
            {
                if (clickedEntryIndex < _foundJournalAsset.entryContent.Length)
                {
                    //textArea.text = string.Join("\n", _foundJournalAsset.entryContent[clickedEntryIndex]);
                    textArea.text += " " + _foundJournalAsset.entryContent[clickedEntryIndex];
                    // journalContent += " " + _foundJournalAsset.entryContent[clickedEntryIndex];
                    Debug.Log("clickedID:" + clickedEntryIndex);
                    clickedEntryIndex++;
                }
                else
                {
                    // handle the case when all entries have been displayed
                    Debug.Log("All entries have been displayed.");
                    clickedEntryIndex = 0;
                    _UIController.UpdateUI(_foundJournalAsset);
                    if (_UIController.choicesContainer.childCount == 0)
                    { 
                        allowPlayerInput = !allowPlayerInput;// stop registering clicks before finding new asset?
                    }
                    //allowPlayerInput = !allowPlayerInput; stop registering clicks before finding new asset?
                }
            }

            if (textChecker != null)
            {
                textChecker.CheckAndHandleOverflow(textArea.text);
            }
        }
    }

    public void OpenJournalMenu()
    {
        Debug.Log(isOpen);

        if (!isOpen)
        {
            this.gameObject.SetActive(true);

            StartCoroutine(ShowUIAfterDelay(2f));
            //   textArea.text = string.Join("\n", journalAsset.entryContent[0]);
            cameraController.Enable(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
            journalUI.SetActive(false);
        }
    }

    IEnumerator ShowUIAfterDelay(float delaySec)
    {
        yield return new WaitForSeconds(delaySec);
        journalUI.SetActive(true);
    }

    public JournalAsset FindEntryByID(int entryID)
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
                Debug.LogError("FIX YOUR JOURNAL :D IDS");
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