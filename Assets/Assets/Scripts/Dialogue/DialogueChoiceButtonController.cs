using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueChoiceButtonController {
    private StateController _stateController;
    private DialogueController _dialogueController;
    private UIController _uiController;

    public DialogueChoiceButtonController(StateController stateController, DialogueController dialogueController, UIController uiController) {
        _stateController = stateController;
        _dialogueController = dialogueController;
        _uiController = uiController;
    }

    private bool ShouldSkip(DialogueChoice choice) { // going to change shouldSkip goal a bit. instead of return now checks for null and displays pre determined buttons.
        return !string.IsNullOrEmpty(choice.TriggerState) && !_stateController.GetBoolState(choice.TriggerState);
    } 

    [AllowsNull]
    public Button CreateButton(DialogueChoice choice) {
        if (ShouldSkip(choice)) {
            Debug.Log($"SKIPPED CHOICE BUTTON {choice.ID} due to trigger {choice.TriggerState}");
            return null;
        }

        Button visualElement = new Button();

        visualElement.text = choice.ChoiceText;
        visualElement.AddToClassList("choice-button");
        visualElement.clickable.clicked += () => HandleButton(choice);

        return visualElement;
    }

    public Button CreateExitButton()
    {
        Button visualElement = new Button();

        visualElement.text = "Exit";
        visualElement.AddToClassList("choice-button");
        visualElement.clickable.clicked += () =>
        {
            _dialogueController.UpdateState();
            _uiController.SetShaded(false);
        };

        return visualElement;
    }

    private void HandleButton(DialogueChoice choice) {
        if (choice.SaveState) {
            _stateController.AddBoolState(choice);
        }

        if (!string.IsNullOrEmpty(choice.NextDialogueID)) {
            var nextDialogue = AssetDatabaseUtility.INSTANCE.GetDialog(choice.NextDialogueID);
            _dialogueController.addDialogue(nextDialogue, _dialogueController.GetActiveCamera());
        } else {
            Debug.Log($"DIALOG EXIT");
        }

        _dialogueController.UpdateState();
        _uiController.SetShaded(false);
    }
}