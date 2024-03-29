using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum KeyType
{
    Blue,
    Green
}
public class Key : Item
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private KeyType keyType;
    public KeyType KeyType => keyType;
    
    private void Awake()
    {
        GetItemType = ItemType.Key;
        playerInventory = FindObjectOfType<Inventory>();
    }
    public override void Interact()
    {
        playerInventory.AddItem(this);
        Destroy(gameObject);
    }
    public override void CloseInteraction(){}
    public override void StopCloseInteraction(){}
    public override void StopInteracting(){}
}
