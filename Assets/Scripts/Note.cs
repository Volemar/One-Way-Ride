using TMPro;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private Canvas noteCanvas;
    [SerializeField] private TextMeshPro text;
    private bool isBeenInteracted = false;
    private bool isBeenCloseInteracted = false;
    private Transform initialTransform;
    private bool isExplorable = true;
    private bool hasCloseInteraction = true;

    bool IInteractable.isExplorable => true;

    bool IInteractable.hasCloseInteraction { get => true; set => hasCloseInteraction = value; }

    public Transform GetInitialTransform()
    {
        return initialTransform;
    }

    private void Start()
    {
        initialTransform = transform;
    }

    public void Interact()
    {
        isBeenInteracted = true;
    }
    public void StopInteracting()
    {
        isBeenInteracted = false;
    }
    public void CloseInteraction()
    {
        isBeenCloseInteracted = true;
        text.text = "Today is your lucky day! You will be a witness of a great deeds, not words. I’m not a goverment, so I’ll won’t make you waiting the next term, don’t worry, you’ll see everything soon!";
        text.enabled = true;
        noteCanvas.enabled = true;
    }
    public void StopCloseInteraction()
    {
        isBeenCloseInteracted = false;
        text.text = "";
        text.enabled = false;
        noteCanvas.enabled = false;
    }

    void IInteractable.Interact()
    {
        throw new System.NotImplementedException();
    }

    void IInteractable.CloseInteraction()
    {
        throw new System.NotImplementedException();
    }

    void IInteractable.StopCloseInteraction()
    {
        throw new System.NotImplementedException();
    }

    void IInteractable.StopInteracting()
    {
        throw new System.NotImplementedException();
    }
}
