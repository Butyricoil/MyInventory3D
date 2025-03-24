using UnityEngine;

public class ItemLogger : MonoBehaviour
{
    private void Start()
    {
        InventoryManager inventory = FindObjectOfType<InventoryManager>();
        if (inventory != null)
        {
            inventory.OnItemAdded.AddListener(LogItemAdded);
            inventory.OnInventoryToggle.AddListener(LogInventoryState);
        }
    }

    private void LogItemAdded(ItemData item)
    {
        Debug.Log($"Item added to inventory: {item.ItemName} (ID: {item.ItemID})");
    }

    private void LogInventoryState(bool isOpen)
    {
        Debug.Log($"Inventory is now {(isOpen ? "open" : "closed")}");
    }
}