using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private MiniMenuManager _miniMenuManager;

    private bool _isPointerHeld; // Флаг, указывающий, удерживается ли ЛКМ
    private bool _isPointerInside; // Флаг, указывающий, находится ли курсор внутри кнопки
    private bool _isPointerOnMiniMenu; // Флаг, указывающий, находится ли курсор над мини-меню
    private float _pointerDownTime; // Время, когда была нажата ЛКМ

    private Coroutine _miniMenuCoroutine; // Корутина для отображения мини-меню с задержкой

    private void Awake()
    {
        // Подписываемся на события мини-меню
        if (_miniMenuManager != null)
        {
            _miniMenuManager.OnPointerEnterMiniMenu += () => _isPointerOnMiniMenu = true;
            _miniMenuManager.OnPointerExitMiniMenu += () => _isPointerOnMiniMenu = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        _isPointerHeld = true;
        _pointerDownTime = Time.time;

        // Запускаем корутину для отображения мини-меню с задержкой
        if (_isPointerInside)
        {
            _miniMenuCoroutine = StartCoroutine(ShowMiniMenuAfterDelay());
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
                // Если время удержания меньше задержки, это клик — открываем обычное меню
                inventoryManager.ToggleMenu();

                // Закрываем мини-меню, если оно открыто
                if (_miniMenuManager.IsMiniMenuVisible())
                {
                    _miniMenuManager.HideMiniMenu();
                }
            }
        }

        // Останавливаем корутину, если она была запущена
        if (_miniMenuCoroutine != null)
        {
            StopCoroutine(_miniMenuCoroutine);
            _miniMenuCoroutine = null;
        }

        _isPointerHeld = false;

        // Скрываем мини-меню, если курсор не внутри кнопки и не над мини-меню
        if (!_isPointerInside && !_isPointerOnMiniMenu)
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
            _miniMenuCoroutine = StartCoroutine(ShowMiniMenuAfterDelay());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerInside = false;

        // Если ЛКМ не удерживается и курсор не над мини-меню, скрываем мини-меню
        if (!_isPointerHeld && !_isPointerOnMiniMenu)
        {
            _miniMenuManager.HideMiniMenu();
        }

        // Останавливаем корутину, если курсор вышел за пределы кнопки
        if (_miniMenuCoroutine != null)
        {
            StopCoroutine(_miniMenuCoroutine);
            _miniMenuCoroutine = null;
        }
    }

    private IEnumerator ShowMiniMenuAfterDelay()
    {
        // Ждём указанное время задержки
        yield return new WaitForSeconds(_miniMenuManager.Delay);

        // Показываем мини-меню только если кнопка всё ещё удерживается и курсор внутри
        if (_isPointerHeld && _isPointerInside)
        {
            _miniMenuManager.ShowMiniMenu();
        }
    }
}