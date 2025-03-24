using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Добавляем для работы с UnityEvent

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/InventoryData")]
public class InventoryData : ScriptableObject
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();

    // События, которые вызываются при изменении инвентаря
    public UnityEvent<ItemData> OnItemAdded;
    public UnityEvent<ItemData> OnItemRemoved;
    public UnityEvent OnInventoryChanged; // Общее событие, если не важно, какой именно предмет изменился

    public void AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogError("Tried to add a null item!");
            return;
        }

        if (!items.Contains(item))
        {
            items.Add(item);
            Debug.Log("Item added: " + item.ItemName);

            // Вызываем события
            OnItemAdded?.Invoke(item);
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.Log("Item already in inventory: " + item.ItemName);
        }
    }

    public void RemoveItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogError("Tried to remove a null item!");
            return;
        }

        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Item removed: " + item.ItemName);

            // Вызываем события
            OnItemRemoved?.Invoke(item);
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.Log("Item not found in inventory: " + item.ItemName);
        }
    }

    // Дополнительный метод для получения списка предметов (если нужен)
    public List<ItemData> GetItems()
    {
        return new List<ItemData>(items); // Возвращаем копию, чтобы оригинальный список нельзя было изменить извне
    }
}