
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DropItem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 4f;
    [SerializeField] private InventoryManager ControllerSlotUI;
    [SerializeField] private LayerMask dropItemLayer;
    [SerializeField] private float customDropDistance;
    [SerializeField] private ControllerPlayer buttonRemove;

    private void Awake()
    {
        buttonRemove.OnDropItemButtonPressed += InputRemovedItem;
    }

    private void OnDisable()
    {
        buttonRemove.OnDropItemButtonPressed -= InputRemovedItem;
    }

    private void InputRemovedItem()
    {
        RemoveItem();
    }
    
    public bool TryRemoveItem()
    {
        if (ControllerSlotUI.GetSelectSlot() < 0 ||
            ControllerSlotUI.GetSelectSlot() >= ControllerSlotUI.GetSlotUI().Length)
            return false;
        
        InventoryItem slotInItem = GetItemInSlot();
        if (slotInItem == null)
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
       

        var item = slotInItem.GetItem();
        bool isNeedSpawnUnderPlayer = !IsHaveEmptySpaceInForwardDirection();
        DicriminationAndRemoveItem();

        if (isNeedSpawnUnderPlayer)
        {
            var dropPosition = player.position; // Прямо на позиции игрока
            SpawnDropItem(item, dropPosition);
            
        }
        else
        {
            SpawnDropItem(item);
        }
        
        return true;
    }
    
    public void SpawnDropItem(ItemInInventory itemInInventory, Vector3? dropPosition = null)
    {
        Vector3 drop = dropPosition ?? GetDropPosition(); 
        Instantiate(itemInInventory.prefab, drop, Quaternion.identity);
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
        return player.transform.position + player.transform.forward * distance;
    }

    public InventoryItem GetItemInSlot()
    {
        InventorySlot selectedSlot = ControllerSlotUI.GetSlotUI()[ControllerSlotUI.GetSelectSlot()];
        InventoryItem slotInItem = selectedSlot.GetComponentInChildren<InventoryItem>();
        return slotInItem;
    }

    private void DicriminationAndRemoveItem()
    {
        InventoryItem slotInItem = GetItemInSlot();
        slotInItem.DicriminationItemCount();
        if (slotInItem.GetItemCount() <= 0)
            Destroy(slotInItem.gameObject);
        else
            slotInItem.RefreshCount();
    }
}
