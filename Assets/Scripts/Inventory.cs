using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();
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
    public bool HasItem(Item item)
    {
        return inventory.Contains(item);
    }
}
