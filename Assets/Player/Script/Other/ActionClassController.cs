using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionClassController 
{
    public InputAction Action { get; }
    public Action<InputAction.CallbackContext> Handler { get; }

    public ActionClassController(InputAction input, Action<InputAction.CallbackContext> context)
    {
        Action = input;
        Handler = context;
    }
}
