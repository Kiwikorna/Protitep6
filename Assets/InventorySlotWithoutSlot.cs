using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlotWithoutUse : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (transform.childCount == 0)
        {
            inventoryItem.SetAfterDragTransform(transform);
        }
        else
        {
            Transform existedItemTransform = transform.GetChild(0);

            Transform droppedItemSwap = inventoryItem.GetAfterDrag();
            
            inventoryItem.SetAfterDragTransform(transform);
            existedItemTransform.SetParent(droppedItemSwap);

            existedItemTransform.position = droppedItemSwap.position;
            inventoryItem.transform.position = transform.position;

        }
        
    }
}
