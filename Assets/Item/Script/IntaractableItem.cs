using UnityEngine;

public class IntaractableItem : MonoBehaviour, Interactable
{
    [SerializeField] private ItemObject item;
    public void Intarection()
    {
        if (Controller.Instance.GetInteractionHandler())
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject);
        }
        
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
