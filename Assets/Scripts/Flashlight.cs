using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float maxlight = 1f;
    [SerializeField] float maxAngle = 1f;
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minAngle = 40f;
    private PlayerControls _controls;
    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
        _controls = GetComponentInParent<PlayerControls>();
    }

    private void Update()
    {
        if(myLight.enabled)
        {
            DecreaseLightAngle();
            DecreaseLightIntensity();
        }
        if(!_controls.GetPlayerToggleFlashlightThisFrame) return;
        _controls.GetPlayerToggleFlashlightThisFrame = false;
        myLight.enabled = !myLight.enabled;
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
    }
    public void RestoreLightIntensity(float intensityAmount)
    {
        myLight.intensity += intensityAmount;
    }

    private void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle > minAngle)
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }
}
