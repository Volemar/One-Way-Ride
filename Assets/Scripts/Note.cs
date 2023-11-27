using TMPro;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject notePanel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private BoxCollider closeInteractionCollider;
    [SerializeField] private BoxCollider mainCollider;
    private bool isBeenInteracted = false;
    private bool isBeenCloseInteracted = false;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    bool isExplorable => true;
    private bool hasCloseInteraction = true;
    bool IInteractable.isExplorable => true;
    bool IInteractable.hasCloseInteraction => true;
    bool IInteractable.hasToggleCloseInteraction => true;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        closeInteractionCollider.enabled = false;
    }

    public void Interact()
    {
        closeInteractionCollider.enabled = true;
        isBeenInteracted = true;
        Debug.Log("Interacted");
    }
    public void StopInteracting()
    {
        Debug.Log("Stop");
        isBeenInteracted = false;
        text.text = "";
        text.enabled = false;
        notePanel.SetActive(false);
        closeInteractionCollider.enabled = false;
        transform.SetParent(null);
        transform.SetPositionAndRotation(initialPosition, initialRotation);
    }
    public void CloseInteraction()
    {
        isBeenCloseInteracted = true;
        Debug.Log("CloserInteraction");
        text.text = "Today is your lucky day! You will be a witness of a great deeds, not words. I’m not a government, so I’ll won’t make you waiting the next term, don’t worry, you’ll see everything soon!";
        text.enabled = true;
        notePanel.SetActive(true);
        //mainCollider.enabled = false;
    }
    public void StopCloseInteraction()
    {
        isBeenCloseInteracted = false;
        text.text = "";
        text.enabled = false;
        notePanel.SetActive(false);
        closeInteractionCollider.enabled = true;
        Debug.Log("StopCloserInteraction");
    }
    
}
