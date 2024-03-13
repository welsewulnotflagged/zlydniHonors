using UnityEngine;
using UnityEngine.UI;

public class Quester : Interactable
{
    //public GameObject coffee;
    // public Transform spawnPos;
    public ItemAsset itemAsset;
    public DialogueAsset dialogueAssetNoCoffee;
    public DialogueAsset dialogueAssetCoffee;
    private InventoryController _inventoryController;
    private CameraController _cameraController;
    private DialogueController _dialogueController;

    private ReputationController _reputationController;

    // public string stateTicket;
   // public GameObject repCanvas;

    private void Start()
    {
        _dialogueController = FindObjectOfType<DialogueController>();
        _inventoryController = FindObjectOfType<InventoryController>();
        _cameraController = GetComponentInChildren<CameraController>();
        _reputationController = GetComponent<ReputationController>();
    }

    protected override void OnInteract(GameObject source)
    {
        if (!_inventoryController.Has(itemAsset))
        {
            _dialogueController.addDialogue(dialogueAssetNoCoffee, _cameraController);
        }
        else
        {
            _dialogueController.addDialogue(dialogueAssetCoffee, _cameraController);
            _reputationController.questFinished = true;
            _reputationController.isApproved = true;
            _reputationController.HandleApproval();
            _inventoryController.Remove(itemAsset);
        }
    }
}