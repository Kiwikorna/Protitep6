using System;
using UnityEngine;

public class SlotUsing : MonoBehaviour
{
    public event Action OnAnyItemUsed;
    public event Action<ItemInInventory> OnItemUsed; 

    private void Update()
    {
        CheckItemUsing();
    }

    private void CheckItemUsing()
    {
        if (ControllerPlayer.Instance.GetInteractionUseItemWithInventoryHandler())
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
