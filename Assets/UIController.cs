using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour {
    private VisualElement _hud;
    public VisualElement _dialogue;
    private VisualElement _inventoryContainer;
    private VisualElement _inventoryIcon;
    private Label _inventoryTitle;
    private Label _inventoryDescription;
    private VisualElement _inventoryList;
    private Label _dialogueLabel;
    private UIDocument _document;
    private InventoryController _inventoryController;
    private VisualElement _menuContainer;
    private DialogueAsset _dialogueAsset;
    public VisualElement ButtonContainer;

    void OnEnable() {
        _inventoryController = FindObjectOfType<InventoryController>();

        _document = GetComponent<UIDocument>();
        _hud = _document.rootVisualElement.Q<VisualElement>("HUD");
        _dialogue = _document.rootVisualElement.Q<VisualElement>("DIALOGUE");
        _inventoryContainer = _document.rootVisualElement.Q<VisualElement>("InventoryContainer");
        _inventoryList = _document.rootVisualElement.Q<VisualElement>("InventoryList");
        _inventoryIcon = _document.rootVisualElement.Q<VisualElement>("InventoryIcon");
        _inventoryTitle = _document.rootVisualElement.Q<Label>("InventoryTitle");
        _inventoryDescription = _document.rootVisualElement.Q<Label>("InventoryDescription");
        _dialogueLabel = _document.rootVisualElement.Q<Label>("DialogueLabel");
        _menuContainer = _document.rootVisualElement.Q<VisualElement>("MenuContainer");
        ButtonContainer = _document.rootVisualElement.Q<VisualElement>("ButtonContainer");

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
        var invButton = _document.rootVisualElement.Q<Button>("InventoryButton");
        invButton.clicked += ShowInventory;
        var menuButton = _document.rootVisualElement.Q<Button>("MenuButton");
        menuButton.clicked += ShowMenu;
        
        _document.rootVisualElement.Q<Button>("ResumeButton").clicked += ShowHUD;
        _document.rootVisualElement.Q<Button>("ExitButton").clicked += CloseGame;
    }

    private void CloseGame() {
        Application.Quit();
    }

    public void ShowMenu() {
        _hud.style.display = DisplayStyle.None;
        _menuContainer.style.display = DisplayStyle.Flex;
    }
    
    public void ShowHUD() {
        _hud.style.display = DisplayStyle.Flex;
        _dialogue.style.display = DisplayStyle.None;
        _inventoryContainer.style.display = DisplayStyle.None;
        _menuContainer.style.display = DisplayStyle.None;
    }

    public void CloseInventory(MouseDownEvent e) {
        Debug.Log(e.target);
        if ((e.target as VisualElement).name == "InventoryContainer") {
            ShowHUD();
        }
    }

    public void ShowInventory() {
        _hud.style.display = DisplayStyle.None;
        _inventoryContainer.style.display = DisplayStyle.Flex;
        _inventoryList.Clear();

        foreach (var item in _inventoryController.GetAllItems()) {
            var elem = new VisualElement();
            elem.AddToClassList("inventoryItem");
            elem.tooltip = elem.name;
            elem.RegisterCallback((MouseDownEvent e) => { SelectItem(elem, item.Key); });
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

    public void AddButton(DialogueAsset.Choice dialogueChoice, int choiceIndex)
    {
        Button visualElement = new Button();
        ButtonContainer.Add(visualElement);
        visualElement.text = dialogueChoice.choiceText;
        visualElement.clicked += (delegate {InsertInText(choiceIndex);});
    }

    public void InsertInText(int currentID)
    {
        //_dialogue.Children().Where(l => l is Button).ToList().ForEach(button => { _dialogue.Remove(button); });
        ButtonContainer.Clear();
        Debug.Log(ButtonContainer.Children());
        /* DialogueAsset.Choice selectedChoice = _dialogueAsset.choices.Find(choice => choice.id == currentID);
         if (selectedChoice != null)
         {
             Debug.Log($"Choice selected!");

            /* Text choiceContentTextArea = uiController.textArea;
             choiceContentTextArea.text += string.Join("/n ", selectedChoice.choiceContent);
             Debug.Log($"Choice content: {selectedChoice.choiceContent}");
             // _choiceContentAppended = !_choiceContentAppended;

         }
         else
         {
             Debug.Log("no dialogue");
         }*/

    }


}