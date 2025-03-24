using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _itemSprite;
    [SerializeField] private int _itemID;
    [SerializeField] private float _weight;
    [SerializeField] private ItemType _itemType;

    public enum ItemType
    {
        Type1,
        Type2,
        Type3
    }

    // Публичные свойства для доступа
    public string ItemName => _itemName;
    public Sprite ItemSprite => _itemSprite;
    public int ItemID => _itemID;
    public float Weight => _weight;
    public ItemType Type => _itemType;

    // Метод для редактора (опционально)
    public void SetID(int newId)
    {
#if UNITY_EDITOR
        _itemID = newId;
        EditorUtility.SetDirty(this);
#endif
    }
}