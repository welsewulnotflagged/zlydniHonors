using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour {
    private VisualElement _hud;
    private VisualElement _dialogue;
    private VisualElement _inventoryContainer;
    private VisualElement _inventoryList;
    private Label _dialogueLabel;
    private UIDocument _document;
    private InventoryController _inventoryController;

    void OnEnable() {
        _inventoryController = FindObjectOfType<InventoryController>();

        _document = GetComponent<UIDocument>();
        _hud = _document.rootVisualElement.Q<VisualElement>("HUD");
        _dialogue = _document.rootVisualElement.Q<VisualElement>("DIALOGUE");
        _inventoryContainer = _document.rootVisualElement.Q<VisualElement>("InventoryContainer");
        _inventoryList = _document.rootVisualElement.Q<VisualElement>("InventoryList");
        _dialogueLabel = _document.rootVisualElement.Q<Label>("DialogueLabel");

        BindButtons();

        _inventoryContainer.RegisterCallback<MouseDownEvent>(CloseInventory);
    }

    private void BindButtons() {
        var invButton = _document.rootVisualElement.Q<Button>("InventoryButton");
        invButton.clicked += ShowInventory;
    }

    public void ShowHUD() {
        _hud.style.display = DisplayStyle.Flex;
        _dialogue.style.display = DisplayStyle.None;
        _inventoryContainer.style.display = DisplayStyle.None;
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
            elem.style.backgroundImage = new StyleBackground(item.Key.icon); 
            _inventoryList.Add(elem);
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
}