using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    public List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log("Item added: " + item.itemName);
        }
        else
        {
            Debug.Log("Item already in inventory: " + item.itemName);
        }
    }

    public void RemoveItem(ItemData item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Item removed: " + item.itemName);
        }
        else
        {
            Debug.Log("Item not found in inventory: " + item.itemName);
        }
    }
}