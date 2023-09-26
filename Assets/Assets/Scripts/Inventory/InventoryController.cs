using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{   
    public GameObject InventoryObj;
    public Transform ObjContent;

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

    /*public void ListItems()
    {
        for each (var item in Items)
        {
            GameObject obj = Instantiate(InventoryObj, ObjContent);
           /// var itemName = obj.transform.Find("ItemAsset/title").GetComponent<Text>();
        }
    }*/
}
