using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class MiniMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _miniInventoryMenu; // Мини-меню
    [SerializeField] private float _delay = 1f; // Задержка для определения зажатия

    private Coroutine _miniMenuCoroutine; // Корутина для отображения мини-меню

    public float Delay => _delay; // Публичное свойство для доступа к задержке

    private void Awake()
    {
        // Инициализация: скрываем мини-меню при старте
        _miniInventoryMenu.SetActive(false);
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

        _miniInventoryMenu.SetActive(false);
    }

    private IEnumerator ShowMiniMenuWithDelay()
    {
        yield return new WaitForSeconds(_delay);
        _miniInventoryMenu.SetActive(true);
    }
}