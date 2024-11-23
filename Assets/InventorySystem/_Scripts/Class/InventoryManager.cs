
using System;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private InventoryInputUI inputSlotUI;
    

 
    private readonly int _maxSizeItemInSlot = 4;
    private int _selectSlot = -1;

    private readonly Dictionary<Type, int> _itemSlotConsolidate = new Dictionary<Type, int>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("You've already create object");
        }

        Instance = this;


        inputSlotUI.OnInventoryInputButton += ChangeSelectedSlot;
        
        // Пример привязки HealthObject к слоту 0
        ConsolidateItemToSlot(typeof(HealthItemInInventory), 0);
        // Пример привязки другого предмета (например, WeaponObject) к слоту 1
        ConsolidateItemToSlot(typeof(ManaItemInInventory), 1);
    }

    private void OnDestroy()
    {
        inputSlotUI.OnInventoryInputButton -= ChangeSelectedSlot;
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
    
   public void ConsolidateItemToSlot(Type itemType, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < inventorySlots.Length)
        {
            _itemSlotConsolidate[itemType] = slotIndex;
        }
    }

    // Проверка, есть ли у предмета привязанный слот
    public bool TryGetAddConsolidateItemInSlot(ItemInInventory itemInInventory, out InventorySlot boundSlot)
    {
        boundSlot = null;
        if (_itemSlotConsolidate.TryGetValue(itemInInventory.GetType(), out int slotIndex))
        {
            boundSlot = inventorySlots[slotIndex];
            return true;
        }
        return false;
    }

    public bool AddItem(ItemInInventory itemInInventory)
    {
        // Проверяем, есть ли у предмета привязанный слот
        if (TryGetAddConsolidateItemInSlot(itemInInventory, out InventorySlot reservedSlot))
        {
            // Если слот привязан, пытаемся добавить в него предмет
            InventoryItem itemInSlot = reservedSlot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                // Если слот пуст, добавляем новый предмет
                SpawnNewItemToInventory(itemInInventory, reservedSlot);
            
      
                return true;
            }
            else if (itemInSlot.GetItem() == itemInInventory && itemInSlot.GetItemCount() < _maxSizeItemInSlot)
            {
                // Если слот уже содержит этот предмет, увеличиваем количество
                itemInSlot.IncriminationItemCount();
                itemInSlot.RefreshCount();
            
                return true;
            }
        }

            // Ищем другой свободный слот
            for (int i = 2; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                // Если слот не зарезервирован и пустой, можем его использовать
                if (itemInSlot == null)
                {
                    SpawnNewItemToInventory(itemInInventory, slot);
            
                   
                    return true;
                }
            }

            return false;
    }


    public ItemInInventory GetSelectedSlot(bool use)
    {
        if (_selectSlot < 0 || _selectSlot >= inventorySlots.Length)
        {
            return null;
        }


        InventorySlot selectSlot = inventorySlots[_selectSlot];
        InventoryItem itemInSlot = selectSlot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            ItemInInventory itemInInventory = itemInSlot.GetItem();

            if (use == true)
            {
                RefreshAndDestroyItemInSlot(itemInSlot);

            }
            return itemInInventory;
        }

        return null;

    }
    

  


    public void RefreshAndDestroyItemInSlot(InventoryItem itemInSlot)
    {
        itemInSlot.DicriminationItemCount();
        if (itemInSlot.GetItemCount() <= 0)
        {
            Destroy(itemInSlot.gameObject);
        }
        else
        {
            itemInSlot.RefreshCount();
                    
        }
    }

   
    public void SpawnNewItemToInventory(ItemInInventory itemInInventory, InventorySlot slot = null)
    {
        GameObject createNewItemInitiate = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = createNewItemInitiate.GetComponent<InventoryItem>();
        
        inventoryItem.ImageIntializedAndRefreshItem(itemInInventory);

        // Резервируем слот для этого предмета
        slot.ReserveForItem(itemInInventory);
    }
    
    public InventorySlot[] GetSlotUI() => inventorySlots;


    public int GetSelectSlot() => _selectSlot;
}