using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private ItemInInventory _itemInInventory;
    private Transform _afterDragTransform;
    private Image _imageItem; 
    [SerializeField] private TextMeshProUGUI textCount;
     private int _itemCount = 1;
    private void Awake()
    {
        _imageItem = GetComponent<Image>();
    }

    public void RefreshCount()
    {
        textCount.text = _itemCount.ToString();
        bool textActive = _itemCount > 1;
        textCount.gameObject.SetActive(textActive);
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        
            _afterDragTransform = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            _imageItem.raycastTarget = false;
        
            
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_itemInInventory.isLocked)
        {
            this.transform.position = Input.mousePosition;
        }
       
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(_afterDragTransform);
        _imageItem.raycastTarget = true;
    }

    public void ImageIntializedAndRefreshItem(ItemInInventory itemInInventory)
    {
         _itemInInventory = itemInInventory;
        _imageItem.sprite = _itemInInventory.sprite;
        RefreshCount();
    }
    
    public void SetAfterDragTransform(Transform trans) => _afterDragTransform = trans;
    public Transform GetAfterDrag() => _afterDragTransform;

    public ItemInInventory GetItem() => _itemInInventory;
    public int DicriminationItemCount() => _itemCount--;
    public int GetItemCount() => _itemCount;

    public int IncriminationItemCount() => _itemCount++;
}
