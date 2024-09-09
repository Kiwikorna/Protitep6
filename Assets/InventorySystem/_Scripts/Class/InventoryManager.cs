
using System;
using System.Resources;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private InventoryInputUI input;
    private readonly int _maxSizeSlot = 4;
    private int _selectSlot = -1;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("You've already create object");
        }

        Instance = this;


        input.onInventoryButten += ChangeSelectedSlot;
    }

    private void OnDestroy()
    {
        input.onInventoryButten -= ChangeSelectedSlot;
    }

    private void ChangeSelectedSlot(int newValue)
    {
        if (_selectSlot >= 0)
        {
            inventorySlots[_selectSlot].Desealected();
        }
        inventorySlots[newValue].Select();
        _selectSlot = newValue;
    }
    
    public bool AddItem(ItemObject item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.GetItemObject() == item && itemInSlot.count < _maxSizeSlot && item.stackable == true)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
              SpawnNewItem(item,slot);
              return true;
            }
        }

        return false;


    }

    public ItemObject GetSelectedSlot(bool use)
    {
        if (_selectSlot < 0 || _selectSlot >=  inventorySlots.Length)
        {
            return null;
        }
        
            
        InventorySlot selectSlot = inventorySlots[_selectSlot];
        InventoryItem itemInSlot = selectSlot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
          ItemObject item =  itemInSlot.GetItemObject();

            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                    
                }
                    
            }
            return item;
        }

        return null;

    }

  
    public void SpawnNewItem(ItemObject item,InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);

        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        
        inventoryItem.Intialized(item);
    }
    
    public InventorySlot[] GetSlotUI() => inventorySlots;


    public int GetSelectSlot() => _selectSlot;
}
