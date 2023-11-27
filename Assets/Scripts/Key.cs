using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private ItemType itemType;
    [SerializeField] private BoxCollider mainCollider;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public bool isExplorable => true;

    public bool hasCloseInteraction => true;

    public bool hasToggleCloseInteraction => false;
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    public void CloseInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void StopCloseInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void StopInteracting()
    {
        throw new System.NotImplementedException();
    }
}
