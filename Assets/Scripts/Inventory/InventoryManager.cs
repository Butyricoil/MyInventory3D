using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _inventoryMenu;

    [Header("Events")]
    [SerializeField] private UnityEvent<ItemData> _onItemAdded;
    [SerializeField] private UnityEvent<bool> _onInventoryToggle;

    private List<ItemData> _items = new List<ItemData>();

    public IReadOnlyList<ItemData> Items => _items.AsReadOnly();
    public UnityEvent<ItemData> OnItemAdded => _onItemAdded;
    public UnityEvent<bool> OnInventoryToggle => _onInventoryToggle;

    private void Awake()
    {
        if (_inventoryMenu != null)
        {
            _inventoryMenu.SetActive(false);
        }
    }

    public void ToggleMenu()
    {
        bool newState = !_inventoryMenu.activeSelf;
        _inventoryMenu.SetActive(newState);
        _onInventoryToggle.Invoke(newState);
    }

    public bool AddItem(ItemData item)
    {
        if (item == null) return false;

        _items.Add(item);
        _onItemAdded.Invoke(item);

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