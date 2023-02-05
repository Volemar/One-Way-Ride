using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public enum ActionMaps
{
    PlayerDefault,
    Exploring,
    Menu
}

public class PlayerControls : MonoBehaviour
{
    private PlayerInput _input;
    #region Input Properties
        #region Exploring
            public bool GetStopExploring { get; private set; }
            public Vector2 GetObjExploring { get; private set; }
            public bool GetCloserInteraction { get; private set; }
        #endregion
        #region PlayerDefault
            public Vector2 GetPlayerMovement { get; private set; }
            public Vector2 GetMouseDelta { get; private set; }
            public bool GetPlayerToggleFlashlightThisFrame { get; private set; }
            public bool GetPlayerJumpedThisFrame { get; private set; }
            public bool GetPlayerReloadedThisFrame { get; private set; }
            public bool GetPlayerSprintThisFrame { get; private set; }
            public bool GetPlayerUIThisFrame { get; private set; }
            public float GetPlayerWeaponScrollThisFrame { get; private set; }
            public float GetPlayerWeaponSwitchedThisFrame { get; private set; }
            public bool GetPlayerAttackedThisFrame { get; private set; }
            public bool GetPlayerInteractedThisFrame { get; private set; }
        #endregion
    #endregion

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        foreach (InputActionMap item in _input.actions.actionMaps)
        {
            item.Disable();
        }
        _input.currentActionMap.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _input.enabled = true;
    }
    
    private void OnDisable()
    {
        _input.enabled = false;
    }
    public void SwitchToExploring()
    {
        _input.SwitchCurrentActionMap(ActionMaps.Exploring.ToString());
    }
    public void SwitchToPlayerDefault()
    {
        _input.SwitchCurrentActionMap(ActionMaps.PlayerDefault.ToString());
    }
    public void SwitchToMenu()
    {
        _input.SwitchCurrentActionMap(ActionMaps.Menu.ToString());
    }
    public void StopExploring(CallbackContext context)
    {
        GetStopExploring = context.performed;
    }
    public void ObjectExploring(CallbackContext context)
    {
        GetObjExploring = context.ReadValue<Vector2>();
    }
    public void CloserInteraction(CallbackContext context)
    {
        GetCloserInteraction = context.performed;
    }

    public void PlayerMovement(CallbackContext context)
    {
        GetPlayerMovement = context.ReadValue<Vector2>();
    }
    public void MouseDelta(CallbackContext context)
    {
        GetMouseDelta = context.ReadValue<Vector2>();
    }
    
    public void PlayerJumpedThisFrame(CallbackContext context)
    {
        GetPlayerJumpedThisFrame = context.performed;
    }
    
    public void PlayerToggleFlashlightThisFrame(CallbackContext context)
    {
        GetPlayerToggleFlashlightThisFrame = context.performed;
    }
    
    public void PlayerAttackedThisFrame(CallbackContext context)
    {
        GetPlayerAttackedThisFrame = context.performed;
    }
    
    public void PlayerInteractedThisFrame(CallbackContext context)
    {
        GetPlayerInteractedThisFrame = context.performed;
    }
        
    public void PlayerReloadedThisFrame(CallbackContext context)
    {
        GetPlayerReloadedThisFrame = context.performed;
    }

    public void PlayerSprintThisFrame(CallbackContext context)
    {
        GetPlayerSprintThisFrame = context.performed;
    }

    public void PlayerWeaponScrollThisFrame(CallbackContext context)
    {
        GetPlayerWeaponScrollThisFrame = context.ReadValue<float>();
    }

    public void PlayerWeaponSwitchedThisFrame(CallbackContext context)
    {
        GetPlayerWeaponSwitchedThisFrame = context.ReadValue<float>();
    }

    public void PlayerUIThisFrame(CallbackContext context)
    {
        GetPlayerUIThisFrame = context.performed;
        if(GetPlayerUIThisFrame) SwitchToMenu(); //Change to esc on build
    }
}