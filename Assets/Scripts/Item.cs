using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Key,
    Battery,
    Ammo
}

public abstract class Item : MonoBehaviour, IInteractable
{
    private ItemType itemType;
    public ItemType GetItemType { get => itemType; protected set => itemType = value; }
    public bool isExplorable => false;

    public bool hasCloseInteraction => false;

    public bool hasToggleCloseInteraction => false;

    public abstract void CloseInteraction();

    public abstract void Interact();
    public abstract void StopCloseInteraction();

    public abstract void StopInteracting();
}
