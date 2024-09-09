using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InventoryInputUI : MonoBehaviour
{
    private Action onInventoryAnyInputButton;
    public Action<int> onInventoryButten;
    private InputSystem_Actions _uiController;

    private void Awake()
    {
        _uiController = new InputSystem_Actions();
        _uiController.UI.InputMainUI.performed += InventoryMainBar;
        _uiController.UI.InputMainUI.Enable();
    }
    
    private void OnDisable()
    {
        _uiController.UI.InputMainUI.canceled -= InventoryMainBar;
    }

    private void InventoryMainBar(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Получаем введенную строку (например, на цифровой клавиатуре)
            string inputString = context.control.name;

            // Преобразуем строку в число
            if (int.TryParse(inputString, out int number) && number is > 0 and < 8)
            {
                onInventoryAnyInputButton?.Invoke(); // Передаем номер слота
                onInventoryButten?.Invoke(number - 1);
            }
        }
    }
}
