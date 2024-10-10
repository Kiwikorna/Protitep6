
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DropItem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 4f;
    [SerializeField] private InventoryManager InputSlotUI;
    [SerializeField] private LayerMask dropItemLayer;
    [SerializeField] private float customDropDistance;
    [SerializeField] private PlayerInput buttonRemove;

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
        if (InputSlotUI.GetSelectSlot() < 0 ||
            InputSlotUI.GetSelectSlot() >= InputSlotUI.GetSlotUI().Length)
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
    
    public void SpawnDropItem(Item item, Vector3? dropPosition = null)
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
        return player.transform.position + player.transform.forward * distance;
    }

    public InventoryItem GetItemInSlot()
    {
        InventorySlot selectedSlot = InputSlotUI.GetSlotUI()[InputSlotUI.GetSelectSlot()];
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
