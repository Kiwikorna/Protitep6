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

    private void Update()
    {
        TryRemoveItem();
    }

    public bool TryRemoveItem()
    {
        if (inventoryInputUI.GetSelectSlot() < 0 ||
            inventoryInputUI.GetSelectSlot() >= inventoryInputUI.GetSlotUI().Length)
            return false;

        InventorySlot selectedSlot = inventoryInputUI.GetSlotUI()[inventoryInputUI.GetSelectSlot()];

        if (selectedSlot == null)
            return false;
        InventoryItem slotInItem = selectedSlot.GetComponentInChildren<InventoryItem>();
        if (slotInItem == null)
            return false;

        if (!IsHaveEmptySpaceInForwardDirection())
            return false;

        
        return true;

    }

    public bool RemoveItem()
    {

        InventorySlot selectedSlot = inventoryInputUI.GetSlotUI()[inventoryInputUI.GetSelectSlot()];
        InventoryItem slotInItem = selectedSlot.GetComponentInChildren<InventoryItem>();

        if (slotInItem != null)
        {
            if (IsHaveEmptySpaceInForwardDirection())
            {
                var item = GetItemInventory();

         
                    
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
            else
            {
              
                    var item = GetItemInventory();
                    var dropPosition = GetDropPosition();

                    SpawnDropItem(item, dropPosition);

                    slotInItem.count--;
                    SpawnDropItem(item, dropPosition);  
                    if (slotInItem.count <= 0)
                    {
                        Destroy(slotInItem.gameObject);
                    }
                    else
                    {
                        slotInItem.RefreshCount();
                    }
                


            }

            return true;
        }

        return false;
    }

    public void SpawnDropItem(ItemObject item)
    {
        Vector3 drop = GetDropPosition();
        Instantiate(item.prefab,  drop, Quaternion.identity);
    }

    public void SpawnDropItem(ItemObject item, Vector3 dropPosition)
    {
        Instantiate(item.prefab, dropPosition, Quaternion.identity);
    }

    public bool IsHaveEmptySpaceInForwardDirection()
    {
        float dropDistance = 1f;
        Ray ray = new Ray(transform.position, transform.forward);
        bool canDrop = !Physics.Raycast(ray, dropDistance * customDropDistance, dropItemLayer);

        return canDrop;
    }

    private Vector3 GetDropPosition()
    {
        if (IsHaveEmptySpaceInForwardDirection())
        {
            return player.transform.position + player.transform.forward * distance;
        }
        else
        {
            return player.transform.position;
        }
    }

    public ItemObject GetItemInventory()
    {
        InventorySlot selectedSlot = inventoryInputUI.GetSlotUI()[inventoryInputUI.GetSelectSlot()];
        InventoryItem slotInItem = selectedSlot.GetComponentInChildren<InventoryItem>();

        ItemObject item = slotInItem.GetItemObject();

        return item;
    }
}
