using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ButtonInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private MiniMenuManager _miniMenuManager;

    private bool _isPointerHeld; // Флаг, указывающий, удерживается ли ЛКМ
    private bool _isPointerInside; // Флаг, указывающий, находится ли курсор внутри кнопки
    private float _pointerDownTime; // Время, когда была нажата ЛКМ

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        _isPointerHeld = true;
        _pointerDownTime = Time.time; // Запоминаем время нажатия ЛКМ

        // Запускаем корутину для отображения мини-меню с задержкой
        if (_isPointerInside)
        {
            _miniMenuManager.ShowMiniMenu();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        // Вычисляем время удержания ЛКМ
        float holdDuration = Time.time - _pointerDownTime;

        // Если курсор внутри кнопки
        if (_isPointerInside)
        {
            if (holdDuration < _miniMenuManager.Delay)
            {
                // Если время удержания меньше задержки, это клик — открываем инвентарь
                inventoryManager.ToggleMenu();
                _miniMenuManager.HideMiniMenu(); // Скрываем мини-меню
            }
            else
            {
                // Если время удержания больше задержки, это зажатие — мини-меню уже показано
                // Ничего не делаем, мини-меню остаётся открытым
            }
        }

        _isPointerHeld = false;

        // Скрываем мини-меню, если курсор не внутри кнопки
        if (!_isPointerInside)
        {
            _miniMenuManager.HideMiniMenu();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerInside = true;

        // Если ЛКМ удерживается, запускаем корутину для отображения мини-меню
        if (_isPointerHeld)
        {
            _miniMenuManager.ShowMiniMenu();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerInside = false;

        // Если ЛКМ не удерживается, скрываем мини-меню
        if (!_isPointerHeld)
        {
            _miniMenuManager.HideMiniMenu();
        }
    }
}