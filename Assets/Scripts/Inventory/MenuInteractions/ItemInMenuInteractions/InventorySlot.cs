﻿using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();
        inventoryItem.parentAfterDrag = transform;
    }
}