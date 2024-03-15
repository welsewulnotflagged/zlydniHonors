using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : Interactable
{
    public ItemAsset itemAsset;
    public GameObject spawnPos;

    private GameObject spawnedObj;
    //  public GameObject itemObj;

    private InventoryController _inventoryController;
    private DialogueController _dialogueController;
    public DialogueAsset _dialogueAsset;
    private CameraController _cameraController;

    private bool _activeInHierarchy;

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
            if (!HasItem() && !_activeInHierarchy)
            {
                spawnedObj = Instantiate(itemAsset.obj);

                spawnedObj.transform.position = spawnPos.transform.position;

                _activeInHierarchy = true;
            }
            else if (HasItem() || _activeInHierarchy)
            {
                //Debug.Log("already spawned");
                Debug.Log("Is active? "+_activeInHierarchy);
            }
        });
    }

    public bool HasItem()
    {
        return _inventoryController.Has(itemAsset);
    }
}