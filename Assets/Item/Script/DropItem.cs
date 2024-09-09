using System;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 4f;
    [SerializeField] private InventoryInputUI inventoryInputUI;
    [SerializeField] private LayerMask dropItemLayer;
    [SerializeField] private float customDropDistance;

    private void Awake()
    {
        Controller.Instance.OnDropItemButtonPressed += InputRemoveItem;
    }

    private void OnDisable()
    {
        Controller.Instance.OnDropItemButtonPressed -= InputRemoveItem;
    }

    private void InputRemoveItem()
    {
        RemoveItem();
    }
    
    public bool TryRemoveItem()
    {
        if (inventoryInputUI.GetSelectSlot() < 0 ||
            inventoryInputUI.GetSelectSlot() >= inventoryInputUI.GetSlotUI().Length)
            return false;
        
        InventoryItem slotInItem = GetItemInSlot();
        if (slotInItem == null)
            return false;
        
        if (!IsHaveEmptySpaceInForwardDirection())
            return false;

        return true;

    }

    public bool RemoveItem()
    {
        if (!TryRemoveItem())
        {
            return false;
        }
        InventoryItem slotInItem = GetItemInSlot();
        
            if (IsHaveEmptySpaceInForwardDirection())
            {
                var item = slotInItem.GetItemObject();
                ShrinkingObjects();
                SpawnDropItem(item);
            }
            else
            {
                var item = slotInItem.GetItemObject();
                var dropPosition = GetDropPosition();
                ShrinkingObjects();
                SpawnDropItem(item, dropPosition);
            }

            return true;
            
    }

    public void SpawnDropItem(ItemObject item, Vector3? dropPosition = null)
    {
        Vector3 drop = dropPosition ?? GetDropPosition(); 
        Instantiate(item.prefab, drop, Quaternion.identity);
    }

    public bool IsHaveEmptySpaceInForwardDirection()
    {
        float dropDistance = 1f;
        bool canDrop = !Physics.Raycast(transform.position, transform.forward, dropDistance * customDropDistance,
            dropItemLayer);
        return canDrop;
    }

    private Vector3 GetDropPosition()
    {
        if (IsHaveEmptySpaceInForwardDirection())
            return player.transform.position + player.transform.forward * distance;
        else
            return player.transform.position;
    }

    public InventoryItem GetItemInSlot()
    {
        InventorySlot selectedSlot = inventoryInputUI.GetSlotUI()[inventoryInputUI.GetSelectSlot()];
        InventoryItem slotInItem = selectedSlot.GetComponentInChildren<InventoryItem>();
        return slotInItem;
    }

    private void ShrinkingObjects()
    {
        InventoryItem slotInItem = GetItemInSlot();
        slotInItem.count--;
        if (slotInItem.count <= 0)
            Destroy(slotInItem.gameObject);
        else
            slotInItem.RefreshCount();
    }
}
