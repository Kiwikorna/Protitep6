
using System;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 4f;
    [SerializeField] private InventoryManager inventoryInputUI;
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
        

        return true;

    }
    public bool RemoveItem()
    {
        if (!TryRemoveItem())
        {
            return false;
        }

        InventoryItem slotInItem = GetItemInSlot();
       

        var item = slotInItem.GetItemObject();
        bool isNeedSpawnUnderPlayer = !IsHaveEmptySpaceInForwardDirection();
        ShrinkingObjects();

        if (isNeedSpawnUnderPlayer)
        {
            var dropPosition = player.transform.position; // Прямо на позиции игрока
            SpawnDropItem(item, dropPosition);
            
        }
        else
        {
            SpawnDropItem(item);
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
        Debug.Log(canDrop);
        return canDrop;
    }

    private Vector3 GetDropPosition()
    {
        return player.transform.position + player.transform.forward * distance;
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
