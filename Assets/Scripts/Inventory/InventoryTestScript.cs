using System.Collections.Generic;
using UnityEngine;

public class InventoryTestScript : MonoBehaviour
{
    [SerializeField] private List<ItemData> _StartItems = new List<ItemData>();
    private List<ItemData> _InventoryItems = new List<ItemData>();

    private void Start()
    {
        foreach (var item in _StartItems)
        {
            AddItem(item);
        }
    }

    private void AddItem(ItemData itemData)
    {
        _InventoryItems.Add(itemData);
    }
}
