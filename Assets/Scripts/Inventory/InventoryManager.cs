using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject inventoryMenu;

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

    public bool AddItem(Item item)
    {
        if (item == null) return false;

        _items.Add(item);
        onItemAdded.Invoke(item);

        Debug.Log($"Added {item.ItemName} (ID: {item.ItemID}) to inventory");
        return true;
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