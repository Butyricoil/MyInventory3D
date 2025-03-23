using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _miniMenu; // Мини-меню
    [SerializeField] private GameObject _menuPanel; // Основное меню (инвентарь)
    [SerializeField] private float _delay = 1f; // Задержка для определения зажатия

    private bool _isPointerHeld; // Флаг, указывающий, удерживается ли ЛКМ
    private bool _isPointerInside; // Флаг, указывающий, находится ли курсор внутри кнопки
    private float _pointerDownTime; // Время, когда была нажата ЛКМ
    private Coroutine _miniMenuCoroutine; // Корутина для отображения мини-меню

    private void Awake()
    {
        // Инициализация: скрываем оба меню при старте
        _miniMenu.SetActive(false);
        _menuPanel.SetActive(false);
    }

    public void ToggleMenu()
    {
        _menuPanel.SetActive(!_menuPanel.activeSelf);
    }
    private IEnumerator ShowMiniMenuWithDelay()
    {
        yield return new WaitForSeconds(_delay);

        // Если ЛКМ всё ещё удерживается и курсор внутри кнопки, показываем мини-меню
        if (_isPointerHeld && _isPointerInside)
        {
            _miniMenu.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        _isPointerHeld = true;
        _pointerDownTime = Time.time; // Запоминаем время нажатия ЛКМ

        // Запускаем корутину для отображения мини-меню с задержкой
        if (_isPointerInside && _miniMenuCoroutine == null)
        {
            _miniMenuCoroutine = StartCoroutine(ShowMiniMenuWithDelay());
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
            if (holdDuration < _delay)
            {
                // Если время удержания меньше задержки, это клик — открываем инвентарь
                _menuPanel.SetActive(true);
                _miniMenu.SetActive(false); // Скрываем мини-меню
            }
            else
            {
                // Если время удержания больше задержки, это зажатие — мини-меню уже показано
                // Ничего не делаем, мини-меню остаётся открытым
            }
        }

        _isPointerHeld = false;

        // Останавливаем корутину, если она была запущена
        if (_miniMenuCoroutine != null)
        {
            StopCoroutine(_miniMenuCoroutine);
            _miniMenuCoroutine = null;
        }

        // Скрываем мини-меню, если курсор не внутри кнопки
        if (!_isPointerInside)
        {
            _miniMenu.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerInside = true;

        // Если ЛКМ удерживается, запускаем корутину для отображения мини-меню
        if (_isPointerHeld && _miniMenuCoroutine == null)
        {
            _miniMenuCoroutine = StartCoroutine(ShowMiniMenuWithDelay());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerInside = false;

        // Если ЛКМ не удерживается, скрываем мини-меню
        if (!_isPointerHeld)
        {
            _miniMenu.SetActive(false);
        }
    }
}
