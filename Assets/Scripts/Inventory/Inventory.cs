using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _StartItems = new List<Item>();
    private List<Item> _InventoryItems = new List<Item>();

    private void Start()
    {
        foreach (var item in _StartItems)
        {
            AddItem(item);
        }
    }

    private void AddItem(Item item)
    {
        _InventoryItems.Add(item);
    }
}
