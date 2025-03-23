using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _InventoryMenu; // Основное меню (инвентарь)

    private void Awake()
    {
        // Инициализация: скрываем меню при старте
        _InventoryMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        _InventoryMenu.SetActive(!_InventoryMenu.activeSelf);
    }
}