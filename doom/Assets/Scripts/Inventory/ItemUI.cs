using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class InventoryUI : MonoBehaviour
{
    [SeralizeField] private Image image;
    [SeralizeField] private Button button;

    public void Initialize(string inventoryID, Item item, Action<string> removeItemAction)
    {
        image.sprite = item.icon;
        transform.localScale = Vector3.one;

        button.onClick.AddListener(() => removeItemAction.Invoke(inventoryID))
    }

    private void OnDestroy() 
    {
        OnButtonClick(RemoveAllListeners());
    }
}