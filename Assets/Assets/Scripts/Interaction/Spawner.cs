using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : Interactable
{
    public ItemAsset itemAsset;
    public GameObject spawnPos;
    //  public GameObject itemObj;

    private InventoryController _inventoryController;
    private DialogueController _dialogueController;
    public DialogueAsset _dialogueAsset;
    private CameraController _cameraController;

    // Start is called before the first frame update
    void Start()
    {
        _inventoryController = FindObjectOfType<InventoryController>();
        _dialogueController = FindObjectOfType<DialogueController>();
        _cameraController = GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void OnInteract(GameObject source)
    {
        _dialogueController.addDialogue(_dialogueAsset, _cameraController);
        _dialogueController.SetCallback(() =>
        {
            if (!HasItem())
            {
                var spawnedObj = Instantiate(itemAsset.obj);

                spawnedObj.transform.position = spawnPos.transform.position;
            }
            else if (HasItem())
            {
                Debug.Log("already spawned");
            }
        });
    }

    public bool HasItem()
    {
        return _inventoryController.Has(itemAsset);
    }
}