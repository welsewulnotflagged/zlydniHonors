/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControllerB {

    private List<ItemAsset> itemList;

    public void Set() {
        itemList = new List<ItemAsset>();

        AddItem(new ItemAsset { ItemAsset.itemType.Ticket });
        Debug.Log(itemList.Count);
    }

    public void AddItem(ItemAsset item) {
        itemList.Add(item);
    }

   /* public void Remove(ItemAsset item) {
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

    public List<string> GetAllItems() {
        return new List<string>(_items);
    }
}*/