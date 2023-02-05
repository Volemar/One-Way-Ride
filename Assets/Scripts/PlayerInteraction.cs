using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private CameraRaycasting raycast;
    private PlayerControls inputs;
    private const string closeInteractionCollider = "CloseInteractionCollider";
    public bool isInteracting = false;
    public bool isCloseInteracting = false;
    IInteractable currentInteractionObject = null;
    public GameObject currentObject = null;
    private Vector3 interactingObjectPosition;
    float sensitivity = 0.5f;
    public float interactionOffset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<PlayerControls>();
        raycast = GetComponent<CameraRaycasting>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputs.GetStopExploring)
        {
            isInteracting = false;
            currentInteractionObject.StopInteracting();
            currentInteractionObject = null;
            currentObject = null;
            inputs.SwitchToPlayerDefault();
            return;
        }
        if(isInteracting)
        {
            if(isCloseInteracting && inputs.GetPlayerInteractedThisFrame)
            {
                isCloseInteracting = false;
                currentInteractionObject.StopCloseInteraction();
            }
            if(currentInteractionObject.hasCloseInteraction)
            {
                if(raycast.IsHitting && raycast.HitInfo.transform.name == closeInteractionCollider
                && inputs.GetPlayerInteractedThisFrame)
                {
                    currentInteractionObject.CloseInteraction();
                    isCloseInteracting = currentInteractionObject.hasToggleCloseInteraction;
                }
            }
            Vector3 newRotation = Vector3.zero;
            newRotation.y = inputs.GetObjExploring.x * sensitivity;
            Vector3 direction = currentObject.transform.position - transform.position;
            currentObject.transform.Rotate(-newRotation, Space.World);
            currentObject.transform.RotateAround(currentObject.transform.position, Camera.main.transform.right, inputs.GetObjExploring.y * sensitivity);
            
            if(raycast.IsHitting && raycast.HitInfo.transform.name == closeInteractionCollider) Debug.Log("You see it!");
            
            return;
        }
        if(!inputs.GetPlayerInteractedThisFrame) return;
        if(!raycast.IsHitting) return;
        currentInteractionObject = raycast.HitInfo.transform.GetComponentInParent<IInteractable>();
        currentObject = raycast.HitInfo.transform.gameObject;
        if(currentInteractionObject == null) return;
        isInteracting = true;
        inputs.SwitchToExploring();
        interactingObjectPosition = Camera.main.transform.position + Camera.main.transform.forward * interactionOffset;
        currentObject.transform.position = interactingObjectPosition;
        currentObject.transform.LookAt(Camera.main.transform);
        currentInteractionObject.Interact();
    }
}
