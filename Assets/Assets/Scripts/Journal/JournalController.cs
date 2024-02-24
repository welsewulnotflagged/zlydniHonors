using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class JournalController : MonoBehaviour {
    public CameraController cameraController;
    public GameObject journalUI;

    public Text textAreaLeft;
    public Text textAreaRight;
    public bool isOpen;
    public Transform choicesContainer;
    public Button choiceButtonPrefab;

    private StateController _stateController;
    private readonly List<JournalAsset> _unlockedJournals = new(); // keep track on unlocked journals
    private readonly List<string> _entries = new();
    private readonly List<string> _pages = new();
    private JournalAsset _activeAsset; // current asset to display entries from
    private int _currentPage;
    private int _entryIndex;

    private void Start() {
        // init default journal
        AddJournal(AssetDatabaseUtility.INSTANCE.GetJournalAsset("0"));
        _stateController = FindObjectOfType<StateController>();
    }

    private void ChangePage(bool next) {
        SplitByPages();
        Debug.Log(_currentPage);
        if (next) {
            _currentPage += 2;
            //TODO MAKE A LIMIT SO IT DOESN'T SCROLLING OUT OF BOUNDS :(
        } else {
            if (_currentPage - 2 <= 0) {
                return;
            }

            _currentPage -= 2;
        }

        textAreaLeft.text = _pages[_currentPage];
        if (_pages.Count > _currentPage + 1) {
            textAreaRight.text = _pages[_currentPage + 1];
        } else {
            textAreaRight.text = "";
        }
    }

    private void RefreshText() {
        SplitByPages();
        textAreaLeft.text = _pages[_currentPage];
        if (_pages.Count > 1) {
            textAreaRight.text = _pages[_currentPage + 1];
        }
    }

    public void AddJournal(JournalAsset journalAsset) {
        _unlockedJournals.Add(journalAsset);
        _activeAsset = journalAsset;
        _entryIndex = 0;
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0) && !HasActiveButtons()) {
            HandleClick();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            ChangePage(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            ChangePage(false);
        }
    }

    public void HandleClick() {
        int pagesCount = _pages.Count;
        if (_activeAsset && _activeAsset.entryContent.Length > _entryIndex) {
            _entries.Add(_activeAsset.entryContent[_entryIndex++]);
            RefreshText();
        }

        if (_activeAsset && _activeAsset.entryContent.Length == _entryIndex && _activeAsset.choices.Count > 0) {
            UpdateButtons(_activeAsset);
        }

        // switch only non even pages
        if (pagesCount != 0 && _pages.Count % 2 != 0 && pagesCount != _pages.Count) {
            ChangePage(true);
        }
    }

    public void OpenJournalMenu() {
        Debug.Log(isOpen);

        if (!isOpen) {
            gameObject.SetActive(true);
            cameraController.Enable(gameObject);
            StartCoroutine(ShowUIAfterDelay(2f));
        } else {
            gameObject.SetActive(false);
            journalUI.SetActive(false);
        }
    }

    IEnumerator ShowUIAfterDelay(float delaySec) {
        yield return new WaitForSeconds(delaySec);
        journalUI.SetActive(true);
    }

    private void SplitByPages() {
        var text = _entries.Aggregate((a, b) => a + b);
        var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var parts = new StringBuilder();

        _pages.Clear();

        foreach (var word in words) {
            if (parts.Length + word.Length < 400) {
                parts.Append(word);
                parts.Append(' ');
            } else {
                _pages.Add(parts.ToString());
                parts.Clear();
                parts.Append(word);
                parts.Append(' ');
            }
        }

        if (parts.Length > 0) {
            _pages.Add(parts.ToString());
        }
    }

    private void UpdateButtons(JournalAsset selectedEntry) {
        foreach (Transform child in choicesContainer) {
            Destroy(child.gameObject);
        }

        if (selectedEntry.choices == null) return;

        foreach (var choice in selectedEntry.choices) {
            CreateChoiceButton(choice);
            Debug.Log("button created");
        }
    }

    private void CreateChoiceButton(JournalAsset.Choice choiceInfo) {
        if (!string.IsNullOrEmpty(choiceInfo.TriggerState) && !_stateController.GetBoolState(choiceInfo.TriggerState)) {
            Debug.Log($"SKIP journal choice {choiceInfo.id} because of {choiceInfo.TriggerState}");
            return;
        }

        var button = Instantiate(choiceButtonPrefab, choicesContainer);
        button.gameObject.SetActive(true);

        button.GetComponentInChildren<Text>().text = choiceInfo.choiceText;

        button.onClick.AddListener(() => {
            if (choiceInfo.SaveState) {
                _stateController.AddBoolState(choiceInfo);
            }

            OnChoiceSelected(choiceInfo.nextEntryID);
        });
    }

    public void OnChoiceSelected(string nextEntryID) {
        // Ensure this entry is only appended once.

        Debug.Log("Choice already selected. Should display next entry");

        var nextEntry = AssetDatabaseUtility.INSTANCE.GetJournalAsset(nextEntryID);

        if (nextEntry != null) {
            // uiController.waitingForButton = false;
            AddJournal(nextEntry);
            HandleClick();
            foreach (Transform child in choicesContainer) {
                Destroy(child.gameObject);
            }
        } else {
            Debug.Log("no entry available");
        }
    }

    public bool HasActiveButtons() {
        return choicesContainer.childCount > 0;
    }
}