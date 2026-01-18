using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory")]

public class Item : ScriptableObject
{
    public string id;
    public string description;

    public Sprite icon;
    public GameObject prefab;
}
