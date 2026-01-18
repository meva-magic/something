using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

[[RequireComponent(typeof(Collider))]]

public class Inventory : MonoBehaviour 
{
    [SerializeField] private InventoryUI ui;

    [SerializeField] private GameObject droppedItemPrefab;   

    [SerializeField] private SerializeDictionary<stritn, Item> inventory = new();


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompaerTag("DroppedItem"))
        {
            var droppedItem = other.GetComponent<DroppedItem>();

            if (droppedItem.pickedUp)
            {
                return;
            }

            droppedItem.pickedUp = true;

            AddItem(droppedItem.item);
            Destroy(other.GameObject);

            AudioManager.instance.Play("PickUp");
        }
    }


    private void AddItem()
    {
        var inventoryId = Guid.NewGuid().ToString();
        inventory.Add(inventoryId, item);
        ui.AddUIItem(inventoryId, item);
    }


    public void DropItem(string inventoryItem)
    {
        var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<DroppedItem>();
        var item = inventory.GetValueOrDefault(inventoryID);
        
        droppedItem.Ititialize(item);
        inventory.Remove(inventoryID);
        ui.RemoveUIItem(inventoryID);

        AudioManager.instance.Play("DropItem");
    }
}
