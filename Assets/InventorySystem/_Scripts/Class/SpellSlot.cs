using UnityEngine;
using UnityEngine.EventSystems;

public class SpellSlot : MonoBehaviour,IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (transform.childCount == 0 && item.ItemInInventory is SpellItem)
        {
            item.SetAfterDragTransform(transform);
        }
        else if(transform.childCount != 0 && item.ItemInInventory is SpellItem)
        {
            Transform existedItemTransform = transform.GetChild(0);

            Transform droppedItemSwap = item.GetAfterDrag();
            
            item.SetAfterDragTransform(transform);
            existedItemTransform.SetParent(droppedItemSwap);

            existedItemTransform.position = droppedItemSwap.position;
            item.transform.position = transform.position;

        }
        
    }
}
