using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerPlayer : MonoBehaviour
{
    public static ControllerPlayer Instance { get; private set; }
    private Vector2 _move = Vector2.zero;
    private InputSystem_Actions _inputSystemActions;
    private List<ActionInputs> _inputs;
    private bool _isInteractedHandler = false;
    public event Action OnDropItemButtonPressed ;
    public event Action OnCastBaseSpellButtonPressed;
    private bool _isUseItemWithInventory = false;
    


    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
        _inputs = new List<ActionInputs>
        {
            new (_inputSystemActions.Player.Move, Movement),
            new (_inputSystemActions.Player.Interact,InteractHandler),
            new (_inputSystemActions.Player.DropItemInventory,DropItemHandler),
            new(_inputSystemActions.Player.UseItem,InteractionItemHandler),
            new(_inputSystemActions.Player.CastBaseSpell,CastSpellButton)
           
        };

        foreach (var controller in _inputs)
        {
            controller.inputAction.performed += controller.HandlerAction;
            controller.inputAction.canceled += controller.HandlerAction;
        }
        
        _inputSystemActions.Enable();

        if (Instance != null)
        {
            Debug.LogError("Instance has  already create ");
        }

        Instance = this;
    }
    
    private void OnDisable()
    {
        foreach (var controller in _inputs)
        {
            controller.inputAction.performed += controller.HandlerAction;
            controller.inputAction.canceled += controller.HandlerAction;
        }
        _inputSystemActions.Disable();
    }
    private void InteractHandler(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            _isInteractedHandler = true;
        }
        else if (obj.canceled)
        {
            _isInteractedHandler = false;
        }
    }

    private void DropItemHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnDropItemButtonPressed?.Invoke();
        }
    }

    private void CastSpellButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnCastBaseSpellButtonPressed?.Invoke();
        }
    }

    private void Movement(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
       
    }

    private void InteractionItemHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isUseItemWithInventory = true;
        }
        else if (context.canceled)
        {
            _isUseItemWithInventory = false;
        }
    }

    public bool GetInteractionHandler()
    {
        bool result = _isInteractedHandler;
        _isInteractedHandler = false;

        return result;
    }



    public bool GetInteractionUseItemWithInventoryHandler()
    {
        return _isUseItemWithInventory;
    }

    private void LateUpdate()
    {
        _isUseItemWithInventory = false;
    }

    public Vector2 GetDirection() => _move;
}
