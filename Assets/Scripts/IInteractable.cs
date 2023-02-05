using UnityEngine;

public interface IInteractable
{
    bool isExplorable { get; }
    bool hasCloseInteraction { get; }
    bool hasToggleCloseInteraction { get; }
    public void Interact();
    public void CloseInteraction();
    public void StopCloseInteraction();
    public void StopInteracting();
    public void SetParent(Transform gameObject);
}
