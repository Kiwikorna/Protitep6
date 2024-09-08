using System;
using UnityEngine;

public class InventoryUsing : MonoBehaviour
{
    public event Action onAnyItemUsed;

    public event Action<ItemObject> onItemUsed; 

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
                onAnyItemUsed?.Invoke();
                onItemUsed?.Invoke(itemObject);
            }
        }
    }
}
