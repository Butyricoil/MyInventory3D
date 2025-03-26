using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    [SerializeField] private Item item;

    [Header("Ui")]
    [SerializeField] private Image image;

    [HideInInspector] public Transform parentAfterDrag;

    public void Start()
    {
        InitializeItem(item);
    }

    private void InitializeItem(Item item)
    {
        image.sprite = item.ItemSprite;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}