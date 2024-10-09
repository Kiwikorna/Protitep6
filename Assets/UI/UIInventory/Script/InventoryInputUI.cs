using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InventoryInputUI : MonoBehaviour
{
    private Action _onInventoryInputAnyInputButton;
    public Action<int> OnInventoryInputButton;
    private InputSystem_Actions _uiInventoryInput;

    private void OnEnable()
    {
        _uiInventoryInput.UI.InputMainUI.Enable();
    }

    private void Awake()
    {
        _uiInventoryInput = new InputSystem_Actions();
        _uiInventoryInput.UI.InputMainUI.performed += InventoryMainBar;
       
    }
    
    private void OnDisable()
    {
        _uiInventoryInput.UI.InputMainUI.canceled -= InventoryMainBar;
        _uiInventoryInput.UI.InputMainUI.Disable();
    }

    private void InventoryMainBar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Получаем введенную строку (например, на цифровой клавиатуре)
            string input = context.control.name;

            // Преобразуем строку в число
            if (int.TryParse(input, out int numberchoicebutton) && numberchoicebutton is > 0 and < 8)
            {
                _onInventoryInputAnyInputButton?.Invoke(); // Передаем номер слота
                OnInventoryInputButton?.Invoke(numberchoicebutton - 1);
            }
        }
    }
}
