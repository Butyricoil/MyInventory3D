using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/item")]
public class Item : ScriptableObject
{
    public string itemName;
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