using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MiniMenuManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _miniInventoryMenu; // Мини-меню
    [SerializeField] private float _delay = 1f; // Задержка для определения зажатия

    public System.Action OnPointerEnterMiniMenu; // Событие входа курсора в мини-меню
    public System.Action OnPointerExitMiniMenu;  // Событие выхода курсора из мини-меню

    private Coroutine _miniMenuCoroutine; // Корутина для отображения мини-меню

    public float Delay => _delay; // Публичное свойство для доступа к задержке

    private void Awake()
    {
        // Инициализация: скрываем мини-меню при старте
        if (_miniInventoryMenu != null)
        {
            _miniInventoryMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("Mini Inventory Menu is not assigned in MiniMenuManager.");
        }
    }

    public void ShowMiniMenu()
    {
        if (_miniMenuCoroutine == null)
        {
            _miniMenuCoroutine = StartCoroutine(ShowMiniMenuWithDelay());
        }
    }

    public void HideMiniMenu()
    {
        if (_miniMenuCoroutine != null)
        {
            StopCoroutine(_miniMenuCoroutine);
            _miniMenuCoroutine = null;
        }

        if (_miniInventoryMenu != null)
        {
            _miniInventoryMenu.SetActive(false);
        }
    }

    private IEnumerator ShowMiniMenuWithDelay()
    {
        yield return new WaitForSeconds(_delay);

        if (_miniInventoryMenu != null)
        {
            _miniInventoryMenu.SetActive(true);
        }
    }

    // Реализация интерфейса IPointerEnterHandler
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Вызываем событие входа курсора в мини-меню
        OnPointerEnterMiniMenu?.Invoke();
    }

    // Реализация интерфейса IPointerExitHandler
    public void OnPointerExit(PointerEventData eventData)
    {
        // Вызываем событие выхода курсора из мини-меню
        OnPointerExitMiniMenu?.Invoke();
    }

    // Метод для проверки, активно ли мини-меню
    public bool IsMiniMenuVisible()
    {
        return _miniInventoryMenu != null && _miniInventoryMenu.activeSelf;
    }
}