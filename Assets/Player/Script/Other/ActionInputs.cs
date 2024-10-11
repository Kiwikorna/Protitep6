using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionInputs 
{
    public InputAction inputAction { get; }
    public Action<InputAction.CallbackContext> HandlerAction { get; }

    public ActionInputs(InputAction inputAction, Action<InputAction.CallbackContext> context)
    {
        this.inputAction = inputAction;
        HandlerAction = context;
    }
}
