using UnityEngine;
using UnityEngine.EventSystems;

public class SpellItem : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
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
}
