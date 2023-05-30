using UnityEngine;

public class ItemsManager : Singleton<ItemsManager>
{
    [SerializeField] private ItemObject[] allItems;

    public ItemObject GetItem(int itemID)
    {
        return allItems[itemID];
    }
}
