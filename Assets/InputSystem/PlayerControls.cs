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
    public bool mouseDisabled = false;
    #region Input Properties
        #region Exploring
            public bool GetStopExploring { get; set; }
            public bool GetThrow { get; set; }
            public Vector2 GetObjExploring { get; set; }
            public bool GetCloserInteraction { get; set; }
        #endregion
        #region PlayerDefault
            public Vector2 GetPlayerMovement { get; set; }
            public Vector2 GetMouseDelta { get; set; }
            public bool GetPlayerCrouchThisFrame { get; set; }
            public bool GetPlayerToggleFlashlightThisFrame { get; set; }
            public bool GetPlayerJumpedThisFrame { get; set; }
            public bool GetPlayerReloadedThisFrame { get; set; }
            public bool GetPlayerSprintThisFrame { get; set; }
            public bool GetPlayerUIThisFrame { get; set; }
            public float GetPlayerWeaponScrollThisFrame { get; set; }
            public float GetPlayerWeaponSwitchedThisFrame { get; set; }
            public bool GetPlayerAttackedThisFrame { get; set; }
            public bool GetPlayerInteractedThisFrame { get; set; }
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
        if(context.phase == InputActionPhase.Started)
        GetStopExploring = true;
    }
    public void ObjectExploring(CallbackContext context)
    {
        GetObjExploring = mouseDisabled ? Vector2.zero : context.ReadValue<Vector2>();
    }
    public void CloserInteraction(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetCloserInteraction = true;
    }

    public void PlayerMovement(CallbackContext context)
    {
        GetPlayerMovement = context.ReadValue<Vector2>();
    }
    public void MouseDelta(CallbackContext context)
    {
        GetMouseDelta = mouseDisabled ? Vector2.zero : context.ReadValue<Vector2>();
    }
    public void PlayerCrouch(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerCrouchThisFrame = true;
    }
    
    public void PlayerJumpedThisFrame(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerJumpedThisFrame = true;
    }
    
    public void PlayerToggleFlashlightThisFrame(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerToggleFlashlightThisFrame = true;
    }
    
    public void PlayerAttackedThisFrame(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerAttackedThisFrame = true;
    }
    
    public void PlayerInteractedThisFrame(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerInteractedThisFrame = true;
    }
        
    public void PlayerReloadedThisFrame(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerReloadedThisFrame = true;
    }

    public void PlayerSprintThisFrame(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerSprintThisFrame = true;
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
        GetPlayerUIThisFrame = true;
        if(GetPlayerUIThisFrame) SwitchToMenu(); //Change to esc on build
    }
    public void PlayerThrewThisFrame(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetThrow = true;
    }
}