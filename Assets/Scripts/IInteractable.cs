public interface IInteractable
{
    bool isExplorable { get; }
    bool hasCloseInteraction { get; set; }
    public void Interact();
    public void CloseInteraction();
    public void StopCloseInteraction();
    public void StopInteracting();
}
