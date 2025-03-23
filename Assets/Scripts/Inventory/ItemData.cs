using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int itemID;
    public float weight;
    public ItemType itemType;

    public enum ItemType
    {
        Type1,
        Type2,
        Type3
    }
}