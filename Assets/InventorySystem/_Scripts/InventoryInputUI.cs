using System;
using UnityEngine;

public class InventoryInputUI : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;
    private int _selectSlot = -1;


    private void Update()
    {
        InputUI();
    }

    private void InputUI()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 8)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    private void ChangeSelectedSlot(int newValue)
    {
        if (_selectSlot >= 0)
        {
            inventorySlots[_selectSlot].Desealected();
        }
        inventorySlots[newValue].Select();
        _selectSlot = newValue;
    }

    public InventorySlot[] GetSlotUI() => inventorySlots;


    public int GetSelectSlot() => _selectSlot;
}
