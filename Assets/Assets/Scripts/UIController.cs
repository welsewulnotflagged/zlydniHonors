using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour {
    private VisualElement _hud;
    private VisualElement _dialogue;
    private VisualElement _inventoryContainer;
    private VisualElement _inventoryIcon;
    private Label _inventoryTitle;
    private Label _inventoryDescription;
    private VisualElement _inventoryList;
    private Label _dialogueLabel;
    private VisualElement _root;
    private UIDocument _document;
    private InventoryController _inventoryController;
    private VisualElement _menuContainer;
    private DialogueController _dialogueController;
    private JournalUIController _journalUIController;
    private DialogueChoiceButtonController _dialogueChoiceButtonController;

    private VisualElement _choiceButtons;
    private Label _choiceTitle;
    private StateController _stateController;

    void OnEnable() {
        _inventoryController = FindObjectOfType<InventoryController>();
        _dialogueController = FindObjectOfType<DialogueController>();
        _stateController = FindObjectOfType<StateController>();
        _dialogueChoiceButtonController = new DialogueChoiceButtonController(_stateController, _dialogueController, this);

        _document = GetComponent<UIDocument>();
        _hud = _document.rootVisualElement.Q<VisualElement>("HUD");
        _dialogue = _document.rootVisualElement.Q<VisualElement>("DIALOGUE");
        _root = _document.rootVisualElement.Q<VisualElement>("Root");
        _inventoryContainer = _document.rootVisualElement.Q<VisualElement>("InventoryContainer");
        _inventoryList = _document.rootVisualElement.Q<VisualElement>("InventoryList");
        _inventoryIcon = _document.rootVisualElement.Q<VisualElement>("InventoryIcon");
        _inventoryTitle = _document.rootVisualElement.Q<Label>("InventoryTitle");
        _inventoryDescription = _document.rootVisualElement.Q<Label>("InventoryDescription");
        _dialogueLabel = _document.rootVisualElement.Q<Label>("DialogueLabel");
        _menuContainer = _document.rootVisualElement.Q<VisualElement>("MenuContainer");

        _choiceButtons = _document.rootVisualElement.Q<VisualElement>("ChoiceButtons");
        _choiceTitle = _document.rootVisualElement.Q<Label>("ChoiceTitle");

        BindButtons();

        _inventoryContainer.RegisterCallback<MouseDownEvent>(CloseInventory);
        _menuContainer.RegisterCallback<MouseDownEvent>(CloseMenu);
    }

    private void CloseMenu(MouseDownEvent e) {
        if ((e.target as VisualElement).name == "MenuContainer") {
            ShowHUD();
        }
    }

    private void BindButtons() {
        _document.rootVisualElement.Q<Button>("InventoryButton").clicked += ShowInventory;
        _document.rootVisualElement.Q<Button>("MenuButton").clicked += ShowMenu;
        _document.rootVisualElement.Q<Button>("ResumeButton").clicked += ShowHUD;
        _document.rootVisualElement.Q<Button>("ExitButton").clicked += CloseGame;
    }

    private void CloseGame() {
        Application.Quit();
    }

    private void ShowMenu() {
        HideAllExcept("MenuContainer");
    }

    public void ShowChoices(DialogueAsset dialogueAsset) {
        HideAllExcept("Choice");
        SetShaded(true);

        _choiceTitle.text = dialogueAsset.choicesTitle ?? "";

        foreach (var choice in dialogueAsset.choices) {
            var button = _dialogueChoiceButtonController.CreateButton(choice);
            if (button != null) {
                _choiceButtons.Add(button);
            }
        }
    }

    public void SetShaded(bool shaded) {
        if (shaded) {
            _root.AddToClassList("shaded");
        } else {
            _root.RemoveFromClassList("shaded");
        }
    }

    public void ShowHUD() {
        HideAllExcept("HUD");
    }

    public void CloseInventory(MouseDownEvent e) {
        Debug.Log(e.target);
        if ((e.target as VisualElement).name == "InventoryContainer") {
            ShowHUD();
        }
    }

    private void HideAllExcept(params string[] ids) {
        foreach (var element in _root.Children()) {
            element.style.display = ids.Contains(element.name) ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    public void ShowInventory() {
        HideAllExcept("InventoryContainer");
        _inventoryList.Clear();

        foreach (var item in _inventoryController.GetAllItems()) {
            var elem = new VisualElement();
            elem.AddToClassList("inventoryItem");
            elem.tooltip = elem.name;
            elem.RegisterCallback((MouseDownEvent _) => { SelectItem(elem, item.Key); });
            elem.style.backgroundImage = new StyleBackground(item.Key.icon);
            _inventoryList.Add(elem);
        }
    }

    public void SelectItem(VisualElement visualElement, ItemAsset itemAsset) {
        _inventoryIcon.style.backgroundImage = new StyleBackground(itemAsset.icon);
        _inventoryTitle.text = itemAsset.title;
        _inventoryDescription.text = itemAsset.description;

        visualElement.AddToClassList("selectedInventoryItem");
        foreach (var item in _inventoryList.Children()) {
            if (item != visualElement) {
                item.RemoveFromClassList("selectedInventoryItem");
            }
        }
    }

    public void ShowDialogue() {
        _dialogue.style.display = DisplayStyle.Flex;
        _hud.style.display = DisplayStyle.None;
    }

    public bool IsDialogueActive() {
        return _dialogue.style.display == DisplayStyle.Flex;
    }

    public void SetDialogueText(string text) {
        _dialogueLabel.text = text;
    }


    public void ClearDialogueButtons() {
        _choiceButtons.Clear();
    }

    public bool HasActiveChoices() {
        return _choiceButtons.childCount > 0;
    }
}