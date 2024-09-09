using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private ItemObject _itemObject;
    private Transform _afteDragTransform;
    private Image _image;

    [SerializeField] private TextMeshProUGUI text;
   [HideInInspector] public int count = 1;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void RefreshCount()
    {
        text.text = count.ToString();
        bool textActive = count > 1;
        text.gameObject.SetActive(textActive);
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        _afteDragTransform = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_afteDragTransform);
        _image.raycastTarget = true;
    }

    public void Intialized(ItemObject item)
    {
         _itemObject = item;
        _image.sprite = _itemObject.sprite;
        RefreshCount();
    }

    public int AddCountItem() => count++;
    public void SetAfterDragTransform(Transform trans) => _afteDragTransform = trans;
    public Transform GetAfterDrag() => _afteDragTransform;

    public ItemObject GetItemObject() => _itemObject;
    public int GetCountItem() => count;
}
