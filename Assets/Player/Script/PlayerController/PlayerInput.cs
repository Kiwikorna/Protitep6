using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    private Vector2 _move = Vector2.zero;
    private InputSystem_Actions _inputSystemActions;
    private List<ActionInput> _inputs;
    private bool _isInteractedHandler = false;
    public event Action OnDropItemButtonPressed ;
    public event Action OnCastBaseSpellButtonPressed;
    private bool _isUseInteraction = false;
    


    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
        _inputs = new List<ActionInput>
        {
            new (_inputSystemActions.Player.Move, Movement),
            new (_inputSystemActions.Player.Interact,InteractHandler),
            new (_inputSystemActions.Player.DropItemInventory,DropItemHandler),
            new(_inputSystemActions.Player.UseItem,InteractionItemHandler),
            new(_inputSystemActions.Player.CastBaseSpell,CastSpellButton)
           
        };

        foreach (var controller in _inputs)
        {
            controller.Action.performed += controller.HandlerAction;
            controller.Action.canceled += controller.HandlerAction;
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
            controller.Action.performed += controller.HandlerAction;
            controller.Action.canceled += controller.HandlerAction;
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
            _isUseInteraction = true;
        }
        else if (context.canceled)
        {
            _isUseInteraction = false;
        }
    }

    public bool GetInteractionHandler()
    {
        bool result = _isInteractedHandler;
        _isInteractedHandler = false;

        return result;
    }



    public bool GetInteractionUseHandler()
    {
        return _isUseInteraction;
    }

    private void LateUpdate()
    {
        _isUseInteraction = false;
    }

    public Vector2 GetDirection() => _move;
}
