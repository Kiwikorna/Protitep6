
using System;
using System.Collections.Generic;
using System.Resources;
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
        ConsolidateItemToSlot(typeof(HealthItem), 0);
        // Пример привязки другого предмета (например, WeaponObject) к слоту 1
        ConsolidateItemToSlot(typeof(ManaItem), 1);
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
    public bool TryGetAddConsolidateIteminSlot(Item item, out InventorySlot boundSlot)
    {
        boundSlot = null;
        if (_itemSlotConsolidate.TryGetValue(item.GetType(), out int slotIndex))
        {
            boundSlot = inventorySlots[slotIndex];
            return true;
        }
        return false;
    }

    public bool AddItem(Item item)
    {
        // Проверяем, есть ли у предмета привязанный слот
        if (TryGetAddConsolidateIteminSlot(item, out InventorySlot reservedSlot))
        {
            // Если слот привязан, пытаемся добавить в него предмет
            InventoryItem itemInSlot = reservedSlot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                // Если слот пуст, добавляем новый предмет
                SpawnNewItemFromInventory(item, reservedSlot);
                return true;
            }
            else if (itemInSlot.GetItem() == item && itemInSlot.GetItemCount() < _maxSizeItemInSlot)
            {
                // Если слот уже содержит этот предмет, увеличиваем количество
                itemInSlot.IncriminationItemCount();
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // Если привязанный слот заполнен или его нет, ищем другой свободный слот
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // Проверяем, что слот пустой и не зарезервирован для других предметов
            if (itemInSlot == null && !slot.IsReservedForOtherItem(item))
            {
                SpawnNewItemFromInventory(item, slot);
                return true;
            }
        }

        return false;
    }

    public Item GetSelectedSlot(bool use)
    {
        if (_selectSlot < 0 || _selectSlot >=  inventorySlots.Length)
        {
            return null;
        }
        
            
        InventorySlot selectSlot = inventorySlots[_selectSlot];
        InventoryItem itemInSlot = selectSlot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
          Item item =  itemInSlot.GetItem();

            if (use == true)
            {
                RefreshAndDestroyItemInSlot(itemInSlot);
                    
            }
            return item;
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
    public void SpawnNewItemFromInventory(Item item, InventorySlot slot)
    {
        GameObject createNewItemInitiate = Instantiate(inventoryItemPrefab, slot.transform);

        InventoryItem inventoryItem = createNewItemInitiate.GetComponent<InventoryItem>();
        inventoryItem.ImageIntializedAndRefreshItem(item);

        // Резервируем слот для этого предмета
        slot.ReserveForItem(item);
    }
    
    public InventorySlot[] GetSlotUI() => inventorySlots;


    public int GetSelectSlot() => _selectSlot;
}
