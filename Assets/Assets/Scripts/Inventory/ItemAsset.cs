using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My assets/Item", order = 100)]
public class ItemAsset : ScriptableObject {

    public string id; 
    public string title;
    public string description;
    public Texture2D icon;
    public GameObject obj;
}
