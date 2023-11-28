using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public enum ActionMaps
{
    PlayerDefault,
    Exploring,
    Inventory,
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
            //public bool GetPlayerInventoryThisFrame { get; set; }
            public bool GetPlayerUsedBattery { get; set; }
            public bool GetPlayerUsedKey { get; set; }
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
    public void SwitchToInventory()
    {
        _input.SwitchCurrentActionMap(ActionMaps.Inventory.ToString());
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
    // public void PlayerInventoryThisFrame(CallbackContext context)
    // {
    //     GetPlayerInventoryThisFrame = context.performed;
    // }
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

    public void PlayerSprint(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerSprintThisFrame = true;
    }

    public void PlayerWeaponScroll(CallbackContext context)
    {
        GetPlayerWeaponScrollThisFrame = context.ReadValue<float>();
    }

    public void PlayerWeaponSwitched(CallbackContext context)
    {
        GetPlayerWeaponSwitchedThisFrame = context.ReadValue<float>();
    }

    public void PlayerUI(CallbackContext context)
    {
        GetPlayerUIThisFrame = true;
        if(GetPlayerUIThisFrame) SwitchToMenu(); //Change to esc on build
    }
    public void PlayerThrew(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetThrow = true;
    }
    public void PlayerUsedBattery(CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        GetPlayerUsedBattery = true;
    }
    // public void PlayerUsedKey(CallbackContext context)
    // {
    //     if(context.phase == InputActionPhase.Started)
    //     GetPlayerUsedKey = true;
    // }
}