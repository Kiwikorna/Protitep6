using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }
    private Vector2 _moveDirection = Vector2.zero;
    private InputSystem_Actions _inputSystemActions;
    private List<ActionClassController> listController;
    private bool _isInteractedHandler = false;
    private bool _isDropItemInventory = false;
    private bool _isUseIntarection = false;


    private void Awake()
    {
        _inputSystemActions = new InputSystem_Actions();
        listController = new List<ActionClassController>
        {
            new (_inputSystemActions.Player.Move, Movement),
            new (_inputSystemActions.Player.Interact,InteractHandler),
            new (_inputSystemActions.Player.DropItemInventory,InteractionDropItemHandler),
            new(_inputSystemActions.Player.UseItem,UseInteractionItem)
        };

        foreach (var controller in listController)
        {
            controller.Action.performed += controller.Handler;
            controller.Action.canceled += controller.Handler;
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
        foreach (var controller in listController)
        {
            controller.Action.performed += controller.Handler;
            controller.Action.canceled += controller.Handler;
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

    private void InteractionDropItemHandler(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isDropItemInventory = true;
        }
        else if (context.canceled)
        {
            _isDropItemInventory = false;
        }
    }

    private void Movement(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
       
    }

    private void UseInteractionItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isUseIntarection = true;
        }
        else if (context.canceled)
        {
            _isUseIntarection = false;
        }
    }

    public bool GetInteractionHandler()
    {
        bool result = _isInteractedHandler;
        _isInteractedHandler = false;

        return result;
    }

    public bool GetInteractionDropHandler()
    {
        bool result = _isDropItemInventory;
        _isDropItemInventory = false;

        return result;
    }
    
    public bool GetInteractionUseHandler()
    {
        bool result = _isUseIntarection;
        _isUseIntarection = false;

        return result;
    }

    public Vector2 GetDirection() => _moveDirection;
}
