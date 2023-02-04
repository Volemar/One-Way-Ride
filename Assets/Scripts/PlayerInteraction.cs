using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private CameraRaycasting raycast;
    private PlayerControls inputs;
    private bool isInteracting = false;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<PlayerControls>();
        raycast = GetComponent<CameraRaycasting>();
    }

    // Update is called once per frame
    void Update()
    {
        IInteractable interactable = raycast.HitInfo.transform.GetComponent<IInteractable>();
        if(interactable == null) return;
        if(isInteracting) return;
        if(!inputs.GetPlayerInteractedThisFrame) return;
        if(!raycast.IsHitting) return;
        isInteracting = true;
        interactable.Interact();
    }
}
