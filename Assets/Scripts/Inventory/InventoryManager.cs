using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private InventorySlot[] inventorySlot;
    [SerializeField] private GameObject InventoryItemPrefab;

    [Header("Events")]
    [SerializeField] private UnityEvent<Item> onItemAdded;
    [SerializeField] private UnityEvent<bool> onInventoryToggle;

    private List<Item> _items = new List<Item>();

    public IReadOnlyList<Item> Items => _items.AsReadOnly();
    public UnityEvent<Item> OnItemAdded => onItemAdded;
    public UnityEvent<bool> OnInventoryToggle => onInventoryToggle;

    private void Awake()
    {
        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(false);
        }
    }

    public void ToggleMenu()
    {
        bool newState = !inventoryMenu.activeSelf;
        inventoryMenu.SetActive(newState);
        onInventoryToggle.Invoke(newState);
    }

    public void AddItem(Item item)
    {
        // найти пустой слот
        for (int i = 0; i < inventorySlot.Length; i++)
        {
            InventorySlot slot = inventorySlot[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }

        if (item == null) return;

        _items.Add(item);
        onItemAdded.Invoke(item);

        Debug.Log($"Added {item.ItemName} (ID: {item.ItemID}) to inventory");
    }



    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialliseItem(item);
    }

    public bool HasItem(int itemId)
    {
        return _items.Exists(item => item.ItemID == itemId);
    }

    public bool RemoveItem(int itemId)
    {
        var item = _items.Find(i => i.ItemID == itemId);
        if (item != null)
        {
            _items.Remove(item);
            return true;
        }
        return false;
    }
}