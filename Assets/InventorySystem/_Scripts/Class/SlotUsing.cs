using System;
using UnityEngine;

public class SlotUsing : MonoBehaviour
{
    public event Action OnAnyItemUsed;
    public event Action<Item> OnItemUsed; 

    private void Update()
    {
        CheckItemUsing();
    }

    private void CheckItemUsing()
    {
        if (PlayerInput.Instance.GetInteractionUseHandler())
        {
            var itemObject = InventoryManager.Instance.GetSelectedSlot(true);
            if (itemObject != null)
            {
                OnAnyItemUsed?.Invoke();
                OnItemUsed?.Invoke(itemObject);
            }
        }
    }
}
