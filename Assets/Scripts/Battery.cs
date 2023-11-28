using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Item
{
    [SerializeField] private Inventory playerInventory;

    private void Awake()
    {
        GetItemType = ItemType.Battery;
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
