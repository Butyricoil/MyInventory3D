using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData itemData; // Ссылка на данные предмета
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Если CanvasGroup отсутствует, добавляем его
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Проверяем, назначены ли данные предмета
        if (itemData == null)
        {
            Debug.LogWarning("ItemData не назначен для DraggableItem!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Делаем объект полупрозрачным
        canvasGroup.blocksRaycasts = false; // Отключаем блокировку лучей
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint))
        {
            rectTransform.localPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Возвращаем прозрачность
        canvasGroup.blocksRaycasts = true; // Включаем блокировку лучей

        // Проверяем, был ли объект отпущен над кнопкой инвентаря
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("InventoryButton"))
        {
            Debug.Log("Предмет отпущен над кнопкой инвентаря");
            // Здесь можно добавить логику для добавления предмета в инвентарь
        }
        else
        {
            Debug.Log("Предмет не был отпущен над кнопкой инвентаря");
            // Возвращаем объект на исходное место или выполняем другие действия
        }
    }
}