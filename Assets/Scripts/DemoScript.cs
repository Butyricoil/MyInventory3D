using UnityEngine;
using UnityEngine.Serialization;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Added item " + itemsToPickup[id]);
        }
        else
        {
            Debug.Log("ITEM DOT ADDED");
        }
    }
}
