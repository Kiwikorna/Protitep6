using System;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 4f;
    [SerializeField] private InventoryInputUI inventoryInputUI;
    [SerializeField] private LayerMask dropItemLayer;
    [SerializeField] private float customDropDistance;

    private void Update()
    {
        RemoveItem();
    }

    public bool RemoveItem()
    {
        if (inventoryInputUI.GetSelectSlot() < 0 || inventoryInputUI.GetSelectSlot() >= inventoryInputUI.GetSlotUI().Length)
            return false;

        
        InventorySlot slot = inventoryInputUI.GetSlotUI()[inventoryInputUI.GetSelectSlot()];
        InventoryItem slotInItem = slot.GetComponentInChildren<InventoryItem>();
        float dropDistance = 1f;
        Ray ray = new Ray(transform.position, transform.forward);
        bool canDrop = !Physics.Raycast(ray,dropDistance * customDropDistance,dropItemLayer);
        if (slotInItem != null)
        {
            if (canDrop)
            {
                ItemObject item = slotInItem.GetItemObject();

                if (Controller.Instance.GetInteractionDropHandler())
                {
                    slotInItem.count--;
                    SpawnDropItem(item);
                    if (slotInItem.count <= 0)
                    {
                        Destroy(slotInItem.gameObject);
                    }
                    else
                    {
                        slotInItem.RefreshCount();
                    }
                }
            }
            else
            {
                // When the character cannot drop the item, throw it under themselves
                ItemObject item = slotInItem.GetItemObject();

                if (Controller.Instance.GetInteractionDropHandler())
                {
                    Vector3 dropPosition = transform.position; // Get the character's current position

                    slotInItem.count--;
                    SpawnDropItem(item,dropPosition); // Use an overloaded version of SpawnDropItem that accepts a position
                    if (slotInItem.count <= 0)
                    {
                        Destroy(slotInItem.gameObject);
                    }
                    else
                    {
                        slotInItem.RefreshCount();
                    }
                }

               
            }
            return true;
        }

        return false;
    }
    public void SpawnDropItem(ItemObject item)
    {
        Vector3 dropPosition = player.transform.position + player.transform.forward * distance;
        Instantiate(item.prefab, dropPosition, Quaternion.identity);
    }
    
    public void SpawnDropItem(ItemObject item, Vector3 dropPosition)
    {
        Instantiate(item.prefab, dropPosition, Quaternion.identity);
    }
    

}
