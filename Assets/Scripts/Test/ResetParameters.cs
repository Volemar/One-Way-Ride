using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParameters : MonoBehaviour,IInteractable
{
    [SerializeField]
    public GameObject target;
    private Transform initialTransform;

    public bool isExplorable => false;

    public bool hasCloseInteraction => false;

    public bool hasToggleCloseInteraction => false;

    public void Start()
    {
        initialTransform = target.transform;
    }

    public void CloseInteraction()
    {
        
    }

    public void Interact()
    {
        target.transform.position = initialTransform.position;
        target.transform.rotation = initialTransform.rotation;
        target.transform.localScale = initialTransform.localScale;
    }

    public void StopCloseInteraction()
    {
        
    }

    public void StopInteracting()
    {
        
    }
}
