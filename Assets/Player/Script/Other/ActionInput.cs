using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionInput 
{
    public InputAction Action { get; }
    public Action<InputAction.CallbackContext> HandlerAction { get; }

    public ActionInput(InputAction input, Action<InputAction.CallbackContext> context)
    {
        Action = input;
        HandlerAction = context;
    }
}
