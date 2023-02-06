using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
public static class CustomTimeScale
{
    private static float _timeScale = 1f;

    public static void ChangeTimeScaleToZero()
    {
        _timeScale = 0;
    }
    public static void ChangeTimeScaleToOne()
    {
        _timeScale = 1;
    }
    public static float GetTimeScale()
    {
        return _timeScale;
    }
}
public class PlayerInteraction : MonoBehaviour
{
    private CameraRaycasting raycast;
    private PlayerControls inputs;
    private const string closeInteractionCollider = "closeInteractionCollider";
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
            CustomTimeScale.ChangeTimeScaleToOne();
            isInteracting = false;
            currentInteractionObject.StopInteracting();
            currentInteractionObject = null;
            currentObject = null;
            inputs.SwitchToPlayerDefault();
            return;
        }
        if(isInteracting)
        {
            if(isCloseInteracting && inputs.GetCloserInteraction)
            {
                inputs.GetCloserInteraction = false;
                isCloseInteracting = false;
                inputs.mouseDisabled = false;
                currentInteractionObject.StopCloseInteraction();
            }
            if(currentInteractionObject.hasCloseInteraction)
            {
                if(raycast.IsHitting && raycast.HitInfo.transform.tag == closeInteractionCollider)
                {
                    Debug.Log("PRESS THIS BUTTON");
                }
                if(inputs.GetCloserInteraction)
                {
                    Debug.Log("HERE WE GO");
                    inputs.GetCloserInteraction = false;
                    inputs.mouseDisabled = true;
                    currentInteractionObject.CloseInteraction();
                    isCloseInteracting = currentInteractionObject.hasToggleCloseInteraction;
                }
            }
            Vector3 newRotation = Vector3.zero;
            newRotation.y = inputs.GetObjExploring.x * sensitivity;
            Vector3 direction = currentObject.transform.position - transform.position;
            currentObject.transform.Rotate(-newRotation, Space.World);
            currentObject.transform.RotateAround(currentObject.transform.position, Camera.main.transform.right, inputs.GetObjExploring.y * sensitivity);
            
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
        CustomTimeScale.ChangeTimeScaleToZero();
    }
}
