using UnityEngine;

public class ItemsManager : Singleton<ItemsManager>
{
    [SerializeField] private ItemObject[] allItems;

    private InventoryItem tmpItem;

    public void DragItem(InventoryItem item)
    {
        tmpItem = item;
    }

    public ItemObject GetItem(int itemID)
    {
        return allItems[itemID];
    }
}
