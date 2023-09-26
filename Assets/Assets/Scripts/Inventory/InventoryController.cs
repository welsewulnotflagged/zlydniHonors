using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance; 
    public List<ItemAsset> Items = new List<ItemAsset>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemAsset item)
    {
        Items.Add(item);
    }

    public void Remove(ItemAsset item)
    {
        Items.Remove(item);
    }
}
