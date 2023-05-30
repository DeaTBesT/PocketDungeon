using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private List<ItemObject> itemsInInventory = new List<ItemObject>();

    public void AddItem(ItemObject item)
    {
        itemsInInventory.Add(item);
    }

    public void RemoveItem(ItemObject item)
    {
        itemsInInventory.Remove(item);
    }
}
