using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ClickableItem : MonoBehaviour, IPointerClickHandler
{
    [FormerlySerializedAs("_itemData")] [SerializeField] private Item item;
    [SerializeField] private InventoryData _inventoryData; // Ссылка на InventoryData
    [SerializeField] private UnityEvent _onPickUp;

    public Item GetItemData() => item;
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

        if (item == null)
        {
            Debug.LogError("Item not assigned!");
            return;
        }

        // Пытаемся добавить предмет в инвентарь
        _inventoryData.AddItem(item);
        _onPickUp.Invoke(); // Вызываем событие (например, звук, эффект)
        Destroy(gameObject); // Уничтожаем предмет на сцене
    }
}