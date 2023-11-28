using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Locker : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private KeyType keyType;
    private Inventory _playerInventory;
    private void Awake()
    {
        _playerInventory = FindObjectOfType<Inventory>();
    }

    public bool isExplorable => false;

    public bool hasCloseInteraction => false;

    public bool hasToggleCloseInteraction => false;

    public void Interact()
    {
        List<Key> keys = _playerInventory.PlayerInventory.Where(x => x.GetItemType == ItemType.Key).Cast<Key>().ToList();
        if(keys.Count == 0) return;
        int counter;
        for (counter = 0; counter < keys.Count; counter++)
        {
            if(keys[counter].KeyType == keyType) _isLocked = false;//If you want, you can remove it from inventory when already used
        }
    }
    public void CloseInteraction(){}

    public void StopCloseInteraction(){}

    public void StopInteracting(){}
}
