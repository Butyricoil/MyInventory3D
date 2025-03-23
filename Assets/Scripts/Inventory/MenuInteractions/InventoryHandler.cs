using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryHandler : MonoBehaviour, IDropHandler
{
    public InventoryData inventoryData;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            DraggableItem draggableItem = droppedObject.GetComponent<DraggableItem>();

            if (draggableItem != null && draggableItem.itemData != null)
            {
                inventoryData.AddItem(draggableItem.itemData);
                Debug.Log("Item added to inventory: " + draggableItem.itemData.itemName);
            }
        }
    }
}