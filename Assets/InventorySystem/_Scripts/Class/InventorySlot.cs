using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]private Image image;
    [SerializeField] private Color selectedSlot, desealectedSlot;
    
   
    [SerializeField] private bool isLocked;
    private ItemInInventory _itemInInventory;
    public void Select()
    {
        image.color = selectedSlot;
    }

    public void Desealected()
    {
        image.color = desealectedSlot;
    }

    private void Awake()
    {
        
        Desealected();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(isLocked)
            return;
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (transform.childCount == 0)
        {
            item.SetAfterDragTransform(transform);
        }
        else
        {
            Transform existedItemTransform = transform.GetChild(0);

            Transform droppedItemSwap = item.GetAfterDrag();
            
            item.SetAfterDragTransform(transform);
            existedItemTransform.SetParent(droppedItemSwap);

            existedItemTransform.position = droppedItemSwap.position;
            item.transform.position = transform.position;

        }
        
    }
    
    // Резервируем слот для определенного предмета
    public void ReserveForItem(ItemInInventory itemInInventory)
    {
        _itemInInventory = itemInInventory;
    }

    // Проверяем, зарезервирован ли слот для другого предмета
    public bool IsReservedForOtherItem(ItemInInventory itemInInventory)
    {
        return  _itemInInventory != itemInInventory;
    }

    // Проверка, зарезервирован ли слот
    public bool IsReserved() => _itemInInventory != null;
}