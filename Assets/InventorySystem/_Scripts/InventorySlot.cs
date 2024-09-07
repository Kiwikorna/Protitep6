using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]private Image image;
    [SerializeField] private Color selectedSlot, desealectedSlot;


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
