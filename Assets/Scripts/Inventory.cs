using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();

    public List<Item> PlayerInventory { get => inventory; private set => inventory = value; }

    public void AddItem(Item item)
    {
        inventory.Add(item);
    }
    public void RemoveItemById(int id)
    {
        inventory.RemoveAt(id);
    }
    public void Remove(Item item)
    {
        inventory.Remove(item);
    }
    public void RemoveByType(ItemType itemType)
    {
        foreach (Item item in inventory)
			{
				if(item.GetItemType == itemType)
				{
					inventory.Remove(item);
				}
			}
    }
    public void RemoveAtIndex(int id)
    {
        inventory.RemoveAt(id);
    }
    public bool HasItem(Item item)
    {
        return inventory.Contains(item);
    }
}
