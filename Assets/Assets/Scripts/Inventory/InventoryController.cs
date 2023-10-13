using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
    private readonly Dictionary<string, int> _items = new Dictionary<string, int>();
   // public GameObject uiInventory;
    public Transform container;
    public Transform containerTemplate;

    /*private void Awake()
    {
        container = transform.Find("Container");
        containerTemplate = container.Find("ContainerTemplate");
    }*/

    public void Set(ItemAsset item, int count) {
        _items[item.id] = count;
        DrawInventory();
    }

    public void Add(ItemAsset item, int count) {
        if (Has(item)) {
            _items[item.id] += count;
        } else {
            _items[item.id] = count;
        }
        DrawInventory();
    }

    public void Remove(ItemAsset item) {
        if (_items.ContainsKey(item.id)) {
            _items.Remove(item.id);
        }
        DrawInventory();
    }

    public int Count(ItemAsset item) {
        if (_items.ContainsKey(item.id)) {
            return _items[item.id];
        }

        return 0;
    }

    public bool Has(ItemAsset item) {
        return _items.ContainsKey(item.id);
    }

    public Dictionary<string, int> GetAllItems() {
        return new Dictionary<string, int>(_items);
    }


    public void DrawInventory(){
        foreach (KeyValuePair<string,int> item in GetAllItems()) 
        {
            RectTransform containerRectTransform = Instantiate(containerTemplate, container).GetComponent<RectTransform>();
            containerRectTransform.gameObject.SetActive(true);
        }
    }
}