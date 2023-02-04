using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerControls : MonoBehaviour
{
    private static PlayerControls _instance;
    public static PlayerControls Instance
    {
        get => _instance;
    }
    public PlayerInput _playerInput;
    private InputActionMap currentActionMap;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _playerInput = new PlayerInput();
        foreach (InputActionMap item in _playerInput)
        {
            item.Disable();
        }
        currentActionMap = _playerInput.PlayerDefault;
        currentActionMap.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }
    
    private void OnDisable()
    {
        _playerInput.Disable();
    }
        
    public bool StopExploration()
    {
        bool isStopped = CheckForStoppedExploring();
        if (isStopped)
        {
            _playerInput.Exploring.Disable();
            _playerInput.PlayerDefault.Enable();
            currentActionMap = _playerInput.PlayerDefault;
        }
        return isStopped;
    }
    public bool CheckForStoppedExploring()
    {
        return _playerInput.Exploring.StopExploring.triggered;
    }
    public Vector2 ObjectExploring()
    {
        return _playerInput.Exploring.Looking.ReadValue<Vector2>();
    }
    public bool CloserInteraction()
    {
        return _playerInput.Exploring.CloserInteraction.triggered;
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerInput.PlayerDefault.Move.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return _playerInput.PlayerDefault.Look.ReadValue<Vector2>();
    }
    
    public bool PlayerJumpedThisFrame()
    {
        return _playerInput.PlayerDefault.Jump.triggered;
    }
    
    public bool PlayerToggleFlashlightThisFrame()
    {
        return _playerInput.PlayerDefault.Flashlight.triggered;
    }
        
    public bool PlayerAttackedThisFrame()
    {
        return _playerInput.PlayerDefault.Attack.triggered;
    }
        
    public bool PlayerInteractedThisFrame(IInteractable interactable)
    {
        bool interacted = _playerInput.PlayerDefault.Interaction.triggered;
        if (interacted && interactable.isExplorable)
        {
            currentActionMap.Disable();
            _playerInput.Exploring.Enable();
            currentActionMap = _playerInput.Exploring;
        }
        return interacted;
    }
        
    public bool PlayerReloadedThisFrame()
    {
        return _playerInput.PlayerDefault.Reload.triggered;
    }

    public bool PlayerSprintThisFrame()
    {
        return _playerInput.PlayerDefault.Sprint.triggered;
    }

    public bool PlayerStopSprintThisFrame()
    {
        return _playerInput.PlayerDefault.Sprint.WasReleasedThisFrame();
    }

    public float PlayerWeaponScrollThisFrame()
    {
        Debug.Log("Scrolled");
        return _playerInput.PlayerDefault.WeaponScroll.ReadValue<float>();
    }

    public float PlayerWeaponSwitchedThisFrame()
    {
        return _playerInput.PlayerDefault.WeaponKeyInput.ReadValue<float>();
    }

    public bool PlayerUIThisFrame()
    {
        bool isMenu = _playerInput.PlayerDefault.Menu.triggered;
        if (isMenu)
        {
            _playerInput.PlayerDefault.Disable();
            _playerInput.UI.Enable();
            currentActionMap = _playerInput.UI;
        }
        return isMenu;
    }
}