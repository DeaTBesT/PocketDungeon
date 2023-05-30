using UnityEngine;

[CreateAssetMenu(menuName = "Items/New item")]
public class ItemObject : ScriptableObject
{
    [SerializeField] private int itemID = -1;

    [Space, SerializeField] private ItemType itemType;

    #region [PublicVars]

    public int ItemID
    {
        get => itemID;
        set => itemID = value;
    }

    public ItemType GetItemType => itemType;

    #endregion

    public enum ItemType
    {
        Item,
        Armor,
        Weapon
    }
}
