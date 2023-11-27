using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Key,
    Battery,
    Ammo
}

public class Item
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private ItemType itemType;
    //TODO Add some picture to show it in inventory menu
    public Item(GameObject gameObject, ItemType itemType)
    {
        this.gameObject = gameObject;
        this.itemType = itemType;
    }
}
