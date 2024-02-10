using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
    private readonly Dictionary<ItemAsset, int> _items = new Dictionary<ItemAsset, int>();
    public void Set(ItemAsset item, int count) {
        _items[item] = count;
    }

    public void Add(ItemAsset item, int count) {
        if (Has(item)) {
            _items[item] += count;
            Debug.Log("added");
        } else {
            _items[item] = count;
            Debug.Log("added");
        }
    }

    public void Remove(ItemAsset item) {
        if (_items.ContainsKey(item)) {
            _items.Remove(item);
        }
    }

    public int Count(ItemAsset item) {
        if (_items.ContainsKey(item)) {
            return _items[item];
        }

        return 0;
    }

    public bool Has(ItemAsset item) {
        return _items.ContainsKey(item);
        Debug.Log("has key");
    }

    public Dictionary<ItemAsset, int> GetAllItems() {
        return new Dictionary<ItemAsset, int>(_items);
    }
}