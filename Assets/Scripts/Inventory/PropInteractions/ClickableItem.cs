using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private InventoryData _inventoryData; // Ссылка на InventoryData
    [SerializeField] private UnityEvent _onPickUp;

    public ItemData GetItemData() => _itemData;
    public UnityEvent OnPickUp => _onPickUp;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TryPickUpItem();
        }
    }

    private void OnMouseDown()
    {
        TryPickUpItem();
    }

    private void TryPickUpItem()
    {
        if (_inventoryData == null)
        {
            Debug.LogError("InventoryData not assigned!");
            return;
        }

        if (_itemData == null)
        {
            Debug.LogError("ItemData not assigned!");
            return;
        }

        // Пытаемся добавить предмет в инвентарь
        _inventoryData.AddItem(_itemData);
        _onPickUp.Invoke(); // Вызываем событие (например, звук, эффект)
        Destroy(gameObject); // Уничтожаем предмет на сцене
    }
}