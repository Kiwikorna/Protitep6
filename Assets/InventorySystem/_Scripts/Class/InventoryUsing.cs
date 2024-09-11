using System;
using UnityEngine;

public class InventoryUsing : MonoBehaviour
{
    public event Action OnAnyItemUsed;
    public event Action<ItemObject> OnItemUsed; 

    private void Update()
    {
        CheckItemUsing();
    }

    private void CheckItemUsing()
    {
        if (Controller.Instance.GetInteractionUseHandler())
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
