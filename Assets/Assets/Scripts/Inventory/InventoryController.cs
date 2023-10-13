using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
    private readonly Dictionary<string, int> _items = new Dictionary<string, int>();

    public void Set(ItemAsset item, int count) {
        _items[item.id] = count;
    }

    public void Add(ItemAsset item, int count) {
        if (Has(item)) {
            _items[item.id] += count;
        } else {
            _items[item.id] = count;
        }
    }

    public void Remove(ItemAsset item) {
        if (_items.ContainsKey(item.id)) {
            _items.Remove(item.id);
        }
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
}