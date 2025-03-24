using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ItemData _itemData;
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
        var inventory = FindObjectOfType<InventoryManager>();
        if (inventory != null && _itemData != null)
        {
            if (inventory.AddItem(_itemData))
            {
                _onPickUp.Invoke();
                Destroy(gameObject);
            }
        }
    }
}