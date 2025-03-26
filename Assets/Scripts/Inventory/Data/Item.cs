using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    [SerializeField] private string itemName;
    [SerializeField] private int itemID;
    [SerializeField] private float weight;
    [SerializeField] private ItemType itemType;

    [Header("Only UI")]
    [SerializeField] private bool stackble;

    [Header("Both")]
    [SerializeField] private Sprite itemSprite;

    public enum ItemType
    {
        Type1,
        Type2,
        Type3
    }

    // Публичные свойства для доступа
    public string ItemName => itemName;
    public Sprite ItemSprite => itemSprite;
    public int ItemID => itemID;
    public float Weight => weight;
    public ItemType Type => itemType;
    public bool Stackable => stackble;


    // Метод для редактора (опционально)
    public void SetID(int newId)
    {
        #if UNITY_EDITOR
        itemID = newId;
        EditorUtility.SetDirty(this);
        #endif
    }
}